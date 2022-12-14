using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract
{
    public interface IUserService
    {
        Task CreateUser(string email);
        Task<PersonalDataDTO?> GetData(Guid userId);
        Task SaveData(Guid userId, PersonalDataDTO userData);
        Task<AuthDTO> Authenticate(string userEmail);
        Task<bool> UserExistsByEmail(string userEmail);
    }
}