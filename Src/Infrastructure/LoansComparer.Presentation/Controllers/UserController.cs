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

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddUserDTO user)
        {
            // TODO: add user email validation
            await _serviceManager.UserService.AddUser(user);
            return Ok();
        } 
    }
}
