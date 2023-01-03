using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("save-data")]
        public async Task<ActionResult> Save([FromBody] SaveUserDTO user)
        {
            // TODO: reject situation when user with email exists and personal data is null (altogether)
            await _serviceManager.UserService.SaveUser(user);
            return Ok();
        }
    }
}
