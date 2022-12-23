using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LoansComparer.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public AuthDTO GenerateTokenForUser(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Some secret veeery secret secret");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new[] { }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            return new AuthDTO { EncryptedToken = encrypterToken, UserEmail = userEmail };
        }

        public async Task<bool> UserExistsByEmail(string userEmail) => await _repositoryManager.UserRepository.UserExistsByEmail(userEmail);
    }
}
