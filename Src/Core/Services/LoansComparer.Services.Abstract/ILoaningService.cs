using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;

namespace LoansComparer.Services.Abstract
{
    public interface ILoaningService
    {
        Task<BaseResponse<CreateInquiryResponse>> Inquire(AddInquiryDTO inquiryData);
    }
}
