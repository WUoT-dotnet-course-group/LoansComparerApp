using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IInquiryRepository
    {
        Task Add(Inquiry inquiry);
        Task<Inquiry> GetById(Guid id);
        Task<List<Inquiry>> GetAllByUser(Guid userId);
    }
}
