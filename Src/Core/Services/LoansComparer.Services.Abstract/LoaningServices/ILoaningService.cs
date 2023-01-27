using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;

namespace LoansComparer.Services.Abstract.LoaningServices
{
    public interface ILoaningBank : IBankApi
    {
        Task<BaseResponse<PaginatedResponse<OfferDTO>>> GetBankOffers(PagingParameter pagingParams);
        Task<BaseResponse> AcceptOffer(Guid offerId);
        Task<BaseResponse> RejectOffer(Guid offerId);
    }
}
