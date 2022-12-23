namespace LoansComparer.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsByEmail(string email);
        Task<Guid> GetUserIdByEmail(string email);
    }
}
