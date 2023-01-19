using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserById(Guid userId);
        Task<bool> UserExistsByEmail(string email);
        Task<User> GetUserByEmail(string email);
    }
}
