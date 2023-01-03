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

        public async Task<User> GetUserById(Guid userId) => await _dbContext.Users.SingleAsync(x => x.ID == userId);

        public async Task<Guid> GetUserIdByEmail(string email)
        {
            var user = await _dbContext.Users.SingleAsync(x => x.Email == email);
            return user.ID;
        }

        public async Task<bool> UserExistsById(Guid userId) => await _dbContext.Users.AnyAsync(x => x.ID == userId);

        public async Task<bool> UserExistsByEmail(string email) => await _dbContext.Users.AnyAsync(x => x.Email == email);
    }
}
