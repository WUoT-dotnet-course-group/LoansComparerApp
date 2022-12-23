namespace LoansComparer.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }

        IInquiryRepository InquiryRepository { get; }

        IBankRepository BankRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
