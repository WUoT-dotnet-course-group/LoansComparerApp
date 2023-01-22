using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.CrossCutting.DTO.LoaningBank.OtherTeam;
using LoansComparer.Domain.Options;
using LoansComparer.Services.Abstract.LoaningServices;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace LoansComparer.Services.LoaningServices
{
    internal sealed class OtherTeamBankService : BaseLoaningService, IBankApi
    {
        private string Token { get; }

        public string Id => "OtherTeamBank";
        protected override string HttpClientId => Id;
        protected override string Name => "Other team SA";

        public OtherTeamBankService(IHttpClientFactory clientFactory, IOptions<OtherTeamBankConfig> configuration) : base(clientFactory)
        {
            Token = configuration.Value.AuthToken;
        }

        protected override Task AuthorizeRequest(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, Token);
            return Task.CompletedTask;
        }

        public async Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData)
        {
            var body = inquiryData.Adapt<CreateInquiryRequest>();
            var response = await SendAsync<OTCreateInquiryResponse, CreateInquiryRequest>(HttpMethod.Post, "api/Applications", body);
            return response.Adapt<BaseResponse<CreateInquiryResponse>>();
        }

        public async Task<BaseResponse<GetInquiryResponse>> GetInquiry(string inquiryId)
        {
            var response = await SendAsync<OTGetInquiryResponse>(HttpMethod.Get, $"api/Applications/{inquiryId}");
            return response.Adapt<BaseResponse<GetInquiryResponse>>();
        }

        public async Task<BaseResponse<OfferDTO>> GetOffer(string offerId)
        {
            var response = await SendAsync<GetOfferResponse>(HttpMethod.Get, $"api/Offers/{offerId}");

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

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<BaseResponse> UploadFile(string offerId, Stream fileStream, string filename)
        {
            using var formData = new MultipartFormDataContent
            {
                { new StreamContent(fileStream), "file", filename }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Offers/{offerId}/document/upload")
            {
                Content = formData,
            };

            return await SendRequestAsync(request);
        }
    }
}
