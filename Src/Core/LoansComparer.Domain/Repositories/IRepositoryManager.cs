namespace LoansComparer.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IInquiryRepository InquiryRepository { get; }

        IBankRepository BankRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
