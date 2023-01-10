using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer.DataPersistence.Repositories
{
    internal sealed class BankRepository : IBankRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public BankRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<Bank> GetById(Guid id)
        {
            //return await _dbContext.Banks.SingleAsync(x => x.ID == id);
            return await _dbContext.Banks.FirstAsync(); // as long as service handles only one bank
        }
    }
}
