using Google.Apis.Auth;
using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IServicesConfiguration _configuration;

        public AuthController(IServiceManager serviceManager, IServicesConfiguration configuration)
        {
            _serviceManager = serviceManager;
            _configuration = configuration;
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<AuthDTO>> SignInWithGoogle([FromBody] string credentials)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration.GetGoogleAuthClientId() }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credentials, settings);

            var userExists = await _serviceManager.UserService.UserExistsByEmail(payload.Email);
            if (!userExists)
            {
                await _serviceManager.UserService.CreateUser(payload.Email);
            }

            var authInfo = await _serviceManager.UserService.Authenticate(payload.Email);
            return Ok(authInfo);
        }
    }
}
