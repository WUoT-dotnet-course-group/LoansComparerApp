namespace LoansComparer.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IInquiryRepository InquiryRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
