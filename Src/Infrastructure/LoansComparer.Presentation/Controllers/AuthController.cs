using Google.Apis.Auth;
using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpPost("signIn")]
        public async Task<ActionResult<AuthDTO>> SignInWithGoogle([FromBody] string credentials)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "926857553613-qeeqtu9t32am5ngfrgrvj7j56hng5i6d.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credentials, settings);

            var userExists = await _serviceManager.UserService.UserExistsByEmail(payload.Email);
            if (userExists)
            {
                var authInfo = await _serviceManager.UserService.Authenticate(payload.Email);

                HttpContext.Response.Cookies.Append("token", authInfo.EncryptedToken,
                     new CookieOptions
                     {
                         Expires = DateTime.Now.AddDays(7),
                         HttpOnly = true,
                         Secure = true,
                         IsEssential = true,
                         SameSite = SameSiteMode.None
                     });

                return Ok(authInfo);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
