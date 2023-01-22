using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.CrossCutting.Utils;
using LoansComparer.Services.Abstract.LoaningServices;
using System.Text;
using System.Text.Json;

namespace LoansComparer.Services.LoaningServices
{
    internal abstract class BaseLoaningService : IBankApi
    {
        protected readonly IHttpClientFactory _clientFactory;

        protected abstract string HttpClientId { get; }
        protected abstract string Name { get; }
        public abstract string Id { get; }

        protected BaseLoaningService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected abstract Task AuthorizeRequest(HttpRequestMessage request);

        public abstract Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData);
        public abstract Task<BaseResponse<GetInquiryResponse>> GetInquiry(string inquiryId);
        public abstract Task<BaseResponse<OfferDTO>> GetOffer(string offerId);
        public abstract Task<BaseResponse> UploadFile(string offerId, Stream fileStream, string filename);

        public async Task<Stream> DownloadFile(string fileUrl)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, fileUrl);
            await AuthorizeRequest(request);

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStreamAsync();
        }

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

            var response = await _clientFactory.CreateClient(HttpClientId).SendAsync(request);
            
            return new BaseResponse()
            {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessStatusCode
            };
        }

        protected async Task<BaseResponse<T>> SendRequestAsync<T>(HttpRequestMessage request) where T : class
        {
            await AuthorizeRequest(request);

            var response = await _clientFactory.CreateClient(HttpClientId).SendAsync(request);
            var content = await response.Content.ReadAsStreamAsync();

            var baseResponse = new BaseResponse<T>()
            {
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                baseResponse.Content = await JsonSerializer.DeserializeAsync<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringIntConverter() }
                });
                baseResponse.IsSuccessful = true;
            }
            return baseResponse;
        }
    }
}
