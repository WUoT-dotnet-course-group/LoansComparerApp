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

        public async Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData)
        {
            var body = inquiryData.Adapt<CreateInquiryRequest>();
            // TODO: fetch hardcoded url from db
            return await SendAsync<CreateInquiryResponse, CreateInquiryRequest>(HttpMethod.Post, "api/inquiries/add", body);
        }

        public async Task<BaseResponse<GetInquiryResponse>> GetInquiry(Guid inquiryId)
        {
            // TODO: fetch hardcoded url from db
            return await SendAsync<GetInquiryResponse>(HttpMethod.Get, $"api/inquiries/{inquiryId}");
        }

        public async Task<BaseResponse<GetOfferResponse>> GetOfferById(Guid offerId)
        {
            // TODO: fetch hardcoded url from db
            return await SendAsync<GetOfferResponse>(HttpMethod.Get, $"api/offers/{offerId}");
        }

        public async Task<BaseResponse<OfferDTO>> GetOfferByInquiryId(Guid inquiryId)
        {
            var inquiryResponse = await GetInquiry(inquiryId);

            if (!inquiryResponse.IsSuccessful || inquiryResponse.Content!.OfferId is null)
            {
                return new()
                {
                    StatusCode = inquiryResponse.StatusCode
                };
            }

            var offerResponse = await GetOfferById(inquiryResponse.Content.OfferId.Value);

            var finalResponse = new BaseResponse<OfferDTO>()
            {
                StatusCode = offerResponse.StatusCode,
            };
            if (offerResponse.IsSuccessful)
            {
                finalResponse.Content = offerResponse.Content!.Adapt<OfferDTO>();
                finalResponse.IsSuccessful = true;
            }

            return finalResponse;
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
