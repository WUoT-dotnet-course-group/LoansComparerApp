using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IBankRepository
    {
        Task<Bank> GetById(Guid id);
    }
}
