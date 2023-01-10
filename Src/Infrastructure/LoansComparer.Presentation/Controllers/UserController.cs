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

        [HttpGet("data/get")]
        public async Task<ActionResult<PersonalDataDTO>> GetData()
        {
            var userID = User.FindFirst("Id")?.Value!;

            var userData = await _serviceManager.UserService.GetData(Guid.Parse(userID));

            return userData switch
            {
                null => NotFound(),
                _ => Ok(userData),
            };
        }

        [HttpPost("data/save")]
        public async Task<ActionResult> SaveData([FromBody] PersonalDataDTO userData)
        {
            var userID = User.FindFirst("Id")?.Value!;

            await _serviceManager.UserService.SaveData(Guid.Parse(userID), userData);

            return Ok();
        }
    }
}
