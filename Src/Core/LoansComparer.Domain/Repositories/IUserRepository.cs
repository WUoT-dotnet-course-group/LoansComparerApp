using LoansComparer.Domain.Entities;

namespace LoansComparer.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<bool> UserExistsByEmail(string email);
        Task<Guid> GetUserIdByEmail(string email);
    }
}
