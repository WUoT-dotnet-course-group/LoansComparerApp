using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task SaveUser(SaveUserDTO user)
        {
            var userByEmail = await GetUserByEmail(user.Email);

            if (userByEmail is null)
            {
                var userToAdd = user.Adapt<User>();
                await _repositoryManager.UserRepository.AddUser(userToAdd);
            }
            else
            {
                userByEmail.PersonalData = user.PersonalData!.Adapt<PersonalData>();
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<AuthDTO> Authenticate(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetGoogleAuthSecretKey());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new[] { }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            var userId = await _repositoryManager.UserRepository.GetUserIdByEmail(userEmail);

            return new AuthDTO { EncryptedToken = encrypterToken, UserEmail = userEmail, UserId = userId };
        }

        public async Task<bool> UserExistsByEmail(string userEmail) => await _repositoryManager.UserRepository.UserExistsByEmail(userEmail);

        public async Task<User?> GetUserByEmail(string userEmail) => await _repositoryManager.UserRepository.GetUserByEmail(userEmail);
    }
}
