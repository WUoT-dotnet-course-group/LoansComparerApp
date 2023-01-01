using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer.DataPersistence.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public UserRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserExistsByEmail(string email) => await _dbContext.Users.AnyAsync(x => x.Email == email);

        public async Task<Guid> GetUserIdByEmail(string email)
        {
            var user = await _dbContext.Users.SingleAsync(x => x.Email == email);
            return user.ID;
        }

        public async Task<User?> GetUserByEmail(string email) => await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    }
}
