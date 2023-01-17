using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoansComparer.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IServicesConfiguration _configuration;

        public UserService(IRepositoryManager repositoryManager, IServicesConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
        }

        public async Task CreateUser(string email)
        {
            await _repositoryManager.UserRepository.AddUser(new User
            {
                Email = email
            });
        }

        public async Task<PersonalDataDTO?> GetData(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserById(userId);

            return user.PersonalData switch
            {
                null => null,
                _ => user.PersonalData.Adapt<PersonalDataDTO>(),
            };
        }

        public async Task SaveData(Guid userId, PersonalDataDTO userData)
        {
            var user = await _repositoryManager.UserRepository.GetUserById(userId);

            user.PersonalData = userData.Adapt<PersonalData>();

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<AuthDTO> Authenticate(string userEmail)
        {
            var user = await _repositoryManager.UserRepository.GetUserByEmail(userEmail);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()), new Claim(ClaimTypes.Role, user.Role.GetEnumDescription()) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GoogleAuthSecretKey)), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthDTO
            {
                EncryptedToken = tokenHandler.WriteToken(token),
                UserEmail = userEmail,
                IsBankEmployee = user.Role is UserRole.BankEmployee
            };
        }

        public async Task<bool> UserExistsByEmail(string userEmail) => await _repositoryManager.UserRepository.UserExistsByEmail(userEmail);
    }
}
