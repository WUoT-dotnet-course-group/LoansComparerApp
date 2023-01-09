namespace LoansComparer.Services.Abstract
{
    public interface IServiceManager
    {
        ILoaningService LoaningService { get; }
        IInquiryService InquiryService { get; }
        IUserService UserService { get; }
    }
}
