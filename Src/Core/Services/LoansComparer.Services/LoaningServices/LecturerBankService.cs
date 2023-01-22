using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.Domain.Options;
using LoansComparer.Services.Abstract.LoaningServices;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace LoansComparer.Services.LoaningServices
{
    internal class LecturerBankService : BaseLoaningService, IBankApi
    {
        private string? Token { get; set; }
        private readonly LecturerBankConfig _configuration;

        public string Id => "LecturerBank";

        protected override string HttpClientId => Id;
        protected override string Name => "Lecturer SA";

        public LecturerBankService(IHttpClientFactory clientFactory, IOptions<LecturerBankConfig> configuration) : base(clientFactory)
        {
            _configuration = configuration.Value;
        }

        protected override async Task AuthorizeRequest(HttpRequestMessage request)
        {
            if (Token is null)
            {
                var client = _clientFactory.CreateClient();

                var authRequest = new HttpRequestMessage(HttpMethod.Post, @"https://indentitymanager.snet.com.pl/connect/token")
                {
                    Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new("client_id", _configuration.ClientId),
                        new("client_secret", _configuration.ClientSecret),
                        new("grant_type", "client_credentials")
                    })
                };

                var authResponse = await client.SendAsync(authRequest);

                var body = await authResponse.Content.ReadAsStringAsync();
                Token = (string)JToken.Parse(body)["access_token"]!;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, Token);
        }

        public async Task<BaseResponse<GetInquiryResponse>> GetInquiry(string inquiryId)
        {
            return await SendAsync<GetInquiryResponse>(HttpMethod.Get, $"/api/v1/Inquire/{inquiryId}");
        }

        public async Task<BaseResponse<OfferDTO>> GetOffer(string offerId)
        {
            var response = await SendAsync<GetOfferResponse>(HttpMethod.Get, $"/api/v1/Offer/{offerId}");

            var finalResponse = response.Adapt<BaseResponse<OfferDTO>>();
            if (finalResponse.IsSuccessful)
            {
                finalResponse.Content!.BankId = Id;
                finalResponse.Content!.BankName = Name;
            }

            return finalResponse;
        }

        public async Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData)
        {
            var body = inquiryData.Adapt<CreateInquiryRequest>();
            return await SendAsync<CreateInquiryResponse, CreateInquiryRequest>(HttpMethod.Post, "/api/v1/Inquire", body);
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
                { new StreamContent(fileStream), "formFile", filename }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/Offer/{offerId}/document/upload")
            {
                Content = formData,
            };

            return await SendRequestAsync(request);
        }
    }
}
