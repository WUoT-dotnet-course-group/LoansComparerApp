using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;

namespace LoansComparer.Services.Abstract
{
    public interface ILoaningService
    {
        Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData);
        Task<BaseResponse<GetInquiryResponse>> GetInquiry(Guid inquiryId);
        Task<BaseResponse<OfferDTO>> GetOfferById(Guid offerId);
        Task<BaseResponse<OfferDTO>> FetchOffer(Guid inquiryId);
        Task<BaseResponse> UploadFile(Guid offerId, Stream fileStream, string filename);
        Task<BaseResponse<PaginatedResponse<OfferDTO>>> GetBankOffers(PagingParameter pagingParams);
        Task<BaseResponse> AcceptOffer(Guid offerId);
        Task<BaseResponse> RejectOffer(Guid offerId);
    }
}
