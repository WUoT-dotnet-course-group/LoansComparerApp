using LoansComparer.CrossCutting.DTO;
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
            var userId = await _repositoryManager.UserRepository.GetUserIdByEmail(userEmail);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetGoogleAuthSecretKey())), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthDTO { EncryptedToken = tokenHandler.WriteToken(token), UserEmail = userEmail };
        }

        public async Task<bool> UserExistsByEmail(string userEmail) => await _repositoryManager.UserRepository.UserExistsByEmail(userEmail);

        public async Task<User?> GetUserByEmail(string userEmail) => await _repositoryManager.UserRepository.GetUserByEmail(userEmail);
    }
}
