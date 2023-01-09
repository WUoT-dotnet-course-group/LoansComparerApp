using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Route("api/dictionary")]
    public class DictionaryController : ControllerBase
    {
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

        [HttpGet("government-id-types")]
        public ActionResult<List<DictionaryDTO>> GetGovernmentIdTypes()
        {
            var jobTypes = Enum.GetValues<GovernmentIdType>().Select(x => new DictionaryDTO()
            {
                Id = (int)x,
                Name = x.GetEnumDescription()
            }).ToList();

            return Ok(jobTypes);
        }
    }
}
