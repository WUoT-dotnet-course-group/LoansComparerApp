using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Route("api/inquiries")]
    public class InquiryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public InquiryController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<ActionResult<List<GetInquiryDTO>>> GetAll()
        {
            var inquiries = await _serviceManager.InquiryService.GetAll();
            return Ok(inquiries);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddInquiryDTO inquiry)
        {
            await _serviceManager.InquiryService.Add(inquiry);
            return Ok();
        }
    }
}
