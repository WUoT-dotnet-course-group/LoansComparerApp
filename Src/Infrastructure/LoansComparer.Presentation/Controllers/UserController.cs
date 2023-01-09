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
        [HttpPost("data/save")]
        public async Task<ActionResult> SaveData([FromBody] PersonalDataDTO userData)
        {
            var userID = User.FindFirst("Id")?.Value!;

            await _serviceManager.UserService.SaveData(Guid.Parse(userID), userData);

            return Ok();
        }
    }
}
