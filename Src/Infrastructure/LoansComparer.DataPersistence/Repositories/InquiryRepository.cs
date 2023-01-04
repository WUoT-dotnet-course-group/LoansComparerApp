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

        public async Task<List<Inquiry>> GetAllByUser(Guid userId) => await _dbContext.Inquiries.Where(x => x.UserID == userId).ToListAsync();

        public async Task<Inquiry> GetById(Guid id)
        {
            // TODO: do validation in advance
            return await _dbContext.Inquiries.SingleAsync(x => x.ID == id);
        }
    }
}
