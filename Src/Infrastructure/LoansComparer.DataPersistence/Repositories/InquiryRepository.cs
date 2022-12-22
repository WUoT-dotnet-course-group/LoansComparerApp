using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer.DataPersistence.Repositories
{
    internal sealed class InquiryRepository : IInquiryRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public InquiryRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Inquiry inquiry)
        {
            await _dbContext.Inquiries.AddAsync(inquiry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Inquiry>> GetAll() => await _dbContext.Inquiries.AsNoTracking().ToListAsync();
    }
}
