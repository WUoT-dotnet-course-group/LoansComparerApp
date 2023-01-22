using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract.LoaningServices
{
    public interface IBankApi
    {
        string Id { get; }

        Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData);
        Task<BaseResponse<GetInquiryResponse>> GetInquiry(string inquiryId);
        Task<BaseResponse<OfferDTO>> GetOffer(string offerId);
        Task<BaseResponse> UploadFile(string offerId, Stream fileStream, string filename);

        async Task<BaseResponse<OfferDTO>> FetchOffer(string inquiryId)
        {
            var inquiryResponse = await GetInquiry(inquiryId);

            if (!inquiryResponse.IsSuccessful || inquiryResponse.Content!.OfferId is null)
            {
                return new()
                {
                    StatusCode = inquiryResponse.StatusCode
                };
            }

            return await GetOffer(inquiryResponse.Content.OfferId);
        }
    }
}
