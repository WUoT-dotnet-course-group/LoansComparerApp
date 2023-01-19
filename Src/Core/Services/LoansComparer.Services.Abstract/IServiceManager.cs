using LoansComparer.Services.Abstract.LoaningServices;

namespace LoansComparer.Services.Abstract
{
    public interface IServiceManager
    {
        ILoaningBank LoaningBankService { get; }
        IBankApi LecturerBankService { get; }
        IInquiryService InquiryService { get; }
        IUserService UserService { get; }
        IEmailService EmailService { get; }
    }
}
