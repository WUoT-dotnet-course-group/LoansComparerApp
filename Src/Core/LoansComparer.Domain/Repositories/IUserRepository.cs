using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<bool> UserExistsByEmail(string email);
        Task<Guid> GetUserIdByEmail(string email);
    }
}
