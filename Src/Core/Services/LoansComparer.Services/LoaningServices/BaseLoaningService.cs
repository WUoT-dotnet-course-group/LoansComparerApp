using LoansComparer.CrossCutting.DTO.LoaningBank;
using System.Text.Json;
using System.Text;

namespace LoansComparer.Services.LoaningServices
{
    internal abstract class BaseLoaningService
    {
        protected readonly IHttpClientFactory _clientFactory;

        protected BaseLoaningService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected abstract Task AuthorizeRequest(HttpRequestMessage request);

        protected async Task<BaseResponse<T>> SendAsync<T>(HttpMethod httpMethod, string url) where T : class
            => await SendRequestAsync<T>(new HttpRequestMessage(httpMethod, url));

        protected async Task<BaseResponse<T>> SendAsync<T, P>(HttpMethod httpMethod, string url, P payload) where T : class
        {
            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"),
            };

            return await SendRequestAsync<T>(request);
        }

        protected async Task<BaseResponse> SendRequestAsync(HttpRequestMessage request)
        {
            await AuthorizeRequest(request);

            var response = await _clientFactory.CreateClient("LoaningBank").SendAsync(request);

            return new BaseResponse()
            {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessStatusCode
            };
        }

        protected async Task<BaseResponse<T>> SendRequestAsync<T>(HttpRequestMessage request) where T : class
        {
            await AuthorizeRequest(request);

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
