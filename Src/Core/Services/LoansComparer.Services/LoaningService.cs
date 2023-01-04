using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.Services.Abstract;
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

        private async Task<BaseResponse<T>> SendAsync<T>(HttpMethod httpMethod, string url) where T : class 
            => await SendRequestAsync<T>(new HttpRequestMessage(httpMethod, url));

        private async Task<BaseResponse<T>> SendAsync<T, P>(HttpMethod httpMethod, string url, P payload) where T : class
        {
            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

            return await SendRequestAsync<T>(request);
        }

        private async Task<BaseResponse<T>> SendRequestAsync<T>(HttpRequestMessage request) where T : class
        {
            var response = await _clientFactory.CreateClient("LoaningBank").SendAsync(request);
            var content = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
            {
                return new BaseResponse<T>() { Content = await JsonSerializer.DeserializeAsync<T>(content) };
            }

            return new BaseResponse<T>() { ErrorStatusCode = response.StatusCode };
        }
    }
}
