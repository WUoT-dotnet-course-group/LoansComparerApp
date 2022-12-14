using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IInquiryRepository
    {
        Task Add(Inquiry inquiry);
        Task<List<Inquiry>> GetAll();
    }
}
