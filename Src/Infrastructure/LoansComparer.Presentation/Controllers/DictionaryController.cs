using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Route("api/dictionary")]
    public class DictionaryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DictionaryController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet("job-types")]
        public ActionResult<List<DictionaryDTO>> GetJobTypes()
        {
            var jobTypes = Enum.GetValues<JobType>().Select(x => new DictionaryDTO()
            {
                Id = (int)x,
                Name = x.GetEnumDescription()
            }).ToList();

            return Ok(jobTypes);
        }
    }
}
