using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.Domain.Options;
using LoansComparer.Services.Abstract.LoaningServices;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace LoansComparer.Services.LoaningServices
{
    internal sealed class LoaningBankService : BaseLoaningService, ILoaningBank
    {
        private string? Token { get; set; }
        private readonly LoaningBankConfig _configuration;

        public string Id => "LoaningBank";

        protected override string HttpClientId => Id;
        protected override string Name => "Loaning SA";

        public LoaningBankService(IHttpClientFactory clientFactory, IOptions<LoaningBankConfig> configuration) : base(clientFactory)
        {
            _configuration = configuration.Value;
        }

        protected async override Task AuthorizeRequest(HttpRequestMessage request)
        {
            if (Token is null)
            {
                var client = _clientFactory.CreateClient(HttpClientId);

                var authRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/token")
                {
                    Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new("ClientId", _configuration.ClientId),
                        new("ClientSecret", _configuration.ClientSecret)
                    })
                };

                var authResponse = await client.SendAsync(authRequest);

                var token = await authResponse.Content.ReadAsStringAsync();
                Token = token.Trim(' ', '\"');
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, Token);
        }

        public async Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData)
        {
            var body = inquiryData.Adapt<CreateInquiryRequest>();
            // TODO: fetch hardcoded url from db
            return await SendAsync<CreateInquiryResponse, CreateInquiryRequest>(HttpMethod.Post, "api/inquiries/add", body);
        }

        public async Task<BaseResponse<GetInquiryResponse>> GetInquiry(string inquiryId)
        {
            // TODO: fetch hardcoded url from db
            return await SendAsync<GetInquiryResponse>(HttpMethod.Get, $"api/inquiries/{inquiryId}");
        }

        public async Task<BaseResponse<OfferDTO>> GetOffer(string offerId)
        {
            // TODO: fetch hardcoded url from db
            var response = await SendAsync<GetOfferResponse>(HttpMethod.Get, $"api/offers/{offerId}");

            var finalResponse = response.Adapt<BaseResponse<OfferDTO>>();
            if (finalResponse.IsSuccessful)
            {
                finalResponse.Content!.BankId = Id;
                finalResponse.Content!.BankName = Name;
            }

            return finalResponse;
        }

        public async Task<Stream> DownloadFile(string fileUrl)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, fileUrl);
            await AuthorizeRequest(request);

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<BaseResponse> UploadFile(string offerId, Stream fileStream, string filename)
        {
            using var formData = new MultipartFormDataContent
            {
                { new StreamContent(fileStream), "file", filename }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/offers/{offerId}/upload")
            {
                Content = formData,
            };

            return await SendRequestAsync(request);
        }

        public async Task<BaseResponse<PaginatedResponse<OfferDTO>>> GetBankOffers(PagingParameter pagingParams)
        {
            var response = await SendAsync<PaginatedResponse<GetOfferDetailsResponse>>(HttpMethod.Get,
                $"api/inquiries?sortOrder={pagingParams.SortOrder}&sortHeader={pagingParams.SortHeader}&pageIndex={pagingParams.PageIndex}&pageSize={pagingParams.PageSize}");
            
            var finalResponse = response.Adapt<BaseResponse<PaginatedResponse<OfferDTO>>>();

            if (finalResponse.IsSuccessful)
            {
                finalResponse.Content!.Items.ForEach(x => x.BankId = Id);
            }

            return finalResponse;
        }

        public async Task<BaseResponse> AcceptOffer(Guid offerId)
            => await SendRequestAsync(new HttpRequestMessage(HttpMethod.Patch, $"api/offers/{offerId}/accept"));

        public async Task<BaseResponse> RejectOffer(Guid offerId)
            => await SendRequestAsync(new HttpRequestMessage(HttpMethod.Patch, $"api/offers/{offerId}/reject"));
    }
}
