using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IInquiryRepository
    {
        Task<Guid> Add(Inquiry inquiry);
        Task<Inquiry> GetById(Guid id);
        Task<PaginatedResponse<Inquiry>> GetByUser<TResult>(Guid userId, int pageIndex, int pageSize, SortOrder sortOrder, string sortHeader);
        Task<int> Count();
    }
}
