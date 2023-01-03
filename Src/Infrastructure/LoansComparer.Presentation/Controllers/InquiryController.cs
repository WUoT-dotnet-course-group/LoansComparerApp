using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/inquiries")]
    public class InquiryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public InquiryController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<ActionResult<List<GetInquiryDTO>>> GetAll()
        {
            // TODO: handle(?) userId == null situation
            var userId = User.FindFirst("Id")?.Value;

            var inquiries = await _serviceManager.InquiryService.GetAllByUser(Guid.Parse(userId!));
            return Ok(inquiries);
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddInquiryDTO inquiry)
        {
            await _serviceManager.InquiryService.Add(inquiry);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("{inquiryId}/chooseOffer")]
        public async Task<ActionResult> ChooseOffer(Guid inquiryId, [FromBody] ChooseOfferDTO chosenOffer)
        {
            await _serviceManager.InquiryService.ChooseOffer(inquiryId, chosenOffer);
            return Ok();
        }
    }
}
