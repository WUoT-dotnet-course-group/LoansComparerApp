using Azure.Core;
using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.Services.Abstract;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LoansComparer.Services
{
    internal sealed class LoaningService : ILoaningService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IServicesConfiguration _configuration;

        private string? Token { get; set; }

        public LoaningService(IHttpClientFactory clientFactory, IServicesConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task AuthorizeRequest(HttpRequestMessage request)
        {
            if (Token is null)
            {
                var client = _clientFactory.CreateClient("LoaningBank");

                var authRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/token")
                {
                    Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new("ClientId", _configuration.LoaningBankClientCredentials.Key),
                        new("ClientSecret", _configuration.LoaningBankClientCredentials.Value)
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

        public async Task<BaseResponse<GetInquiryResponse>> GetInquiry(Guid inquiryId)
        {
            // TODO: fetch hardcoded url from db
            return await SendAsync<GetInquiryResponse>(HttpMethod.Get, $"api/inquiries/{inquiryId}");
        }

        public async Task<BaseResponse<OfferDTO>> GetOfferById(Guid offerId)
        {
            // TODO: fetch hardcoded url from db
            var response = await SendAsync<GetOfferResponse>(HttpMethod.Get, $"api/offers/{offerId}");
            return response.Adapt<BaseResponse<OfferDTO>>();
        }

        public async Task<BaseResponse<OfferDTO>> FetchOffer(Guid inquiryId)
        {
            var inquiryResponse = await GetInquiry(inquiryId);

            if (!inquiryResponse.IsSuccessful || inquiryResponse.Content!.OfferId is null)
            {
                return new()
                {
                    StatusCode = inquiryResponse.StatusCode
                };
            }

            return await GetOfferById(inquiryResponse.Content.OfferId.Value);
        }

        public async Task<BaseResponse> UploadFile(Guid offerId, Stream fileStream, string filename)
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

            return response.Adapt<BaseResponse<PaginatedResponse<OfferDTO>>>();
        }

        public async Task<BaseResponse> AcceptOffer(Guid offerId)
            => await SendRequestAsync(new HttpRequestMessage(HttpMethod.Patch, $"api/offers/{offerId}/accept"));

        public async Task<BaseResponse> RejectOffer(Guid offerId)
            => await SendRequestAsync(new HttpRequestMessage(HttpMethod.Patch, $"api/offers/{offerId}/reject"));

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

        private async Task<BaseResponse> SendRequestAsync(HttpRequestMessage request) 
        {
            await AuthorizeRequest(request);

            var response = await _clientFactory.CreateClient("LoaningBank").SendAsync(request);

            return new BaseResponse()
            {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessStatusCode
            };
        }

        private async Task<BaseResponse<T>> SendRequestAsync<T>(HttpRequestMessage request) where T : class
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
