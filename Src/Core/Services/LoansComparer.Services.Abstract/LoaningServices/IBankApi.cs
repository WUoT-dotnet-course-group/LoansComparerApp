using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract.LoaningServices
{
    public interface IBankApi
    {
        Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData);
        Task<BaseResponse<GetInquiryResponse>> GetInquiry(Guid inquiryId);
        Task<BaseResponse<OfferDTO>> GetOffer(Guid offerId);
        Task<BaseResponse> UploadFile(Guid offerId, Stream fileStream, string filename);

        async Task<BaseResponse<OfferDTO>> FetchOffer(Guid inquiryId)
        {
            var inquiryResponse = await GetInquiry(inquiryId);

            if (!inquiryResponse.IsSuccessful || inquiryResponse.Content!.OfferId is null)
            {
                return new()
                {
                    StatusCode = inquiryResponse.StatusCode
                };
            }

            return await GetOffer(inquiryResponse.Content.OfferId.Value);
        }
    }
}
