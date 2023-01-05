using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.Services.Abstract;
using Mapster;
using System.Text;
using System.Text.Json;

namespace LoansComparer.Services
{
    internal sealed class LoaningService : ILoaningService
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoaningService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<BaseResponse<CreateInquiryResponse>> Inquire(AddInquiryDTO inquiryData)
        {
            var body = inquiryData.Adapt<CreateInquiryRequest>();
            // TODO: fetch hardcoded url from db
            return await SendAsync<CreateInquiryResponse, CreateInquiryRequest>(HttpMethod.Post, "api/inquiries/add", body);
        }

        private async Task<BaseResponse<T>> SendAsync<T>(HttpMethod httpMethod, string url) where T : class 
            => await SendRequestAsync<T>(new HttpRequestMessage(httpMethod, url));

        private async Task<BaseResponse<T>> SendAsync<T, P>(HttpMethod httpMethod, string url, P payload) where T : class
        {
            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"),
            };

            return await SendRequestAsync<T>(request);
        }

        private async Task<BaseResponse<T>> SendRequestAsync<T>(HttpRequestMessage request) where T : class
        {
            var response = await _clientFactory.CreateClient("LoaningBank").SendAsync(request);
            var content = await response.Content.ReadAsStreamAsync();

            var baseResponse = new BaseResponse<T>()
            {
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                baseResponse.Content = await JsonSerializer.DeserializeAsync<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                baseResponse.IsSuccessful = true;
            }
            return baseResponse;
        }
    }
}
