using LoansComparer.Services.Abstract.LoaningServices;

namespace LoansComparer.Services.Abstract
{
    public interface IServiceManager
    {
        IInquiryService InquiryService { get; }
        IUserService UserService { get; }
        IEmailService EmailService { get; }
    }
}
