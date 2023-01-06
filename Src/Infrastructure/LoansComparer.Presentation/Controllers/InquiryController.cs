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
            var userId = User.FindFirst("Id")?.Value!;

            var inquiries = await _serviceManager.InquiryService.GetAllByUser(Guid.Parse(userId));
            return Ok(inquiries);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<CreateInquiryResponseDTO>> Create([FromBody] CreateInquiryDTO inquiry)
        {
            var userId = User.FindFirst("Id")?.Value;
            var inquiryId = await _serviceManager.InquiryService.Add(inquiry, userId);

            var response = await _serviceManager.LoaningService.Inquire(inquiry);
            if (response.IsSuccessful)
            {
                return Ok(new CreateInquiryResponseDTO() { InquiryId = inquiryId, BankInquiryId = response.Content!.InquiryId });
            }

            return StatusCode(500);
        }

        [AllowAnonymous]
        [HttpGet("{bankInquiryId}/offer")]
        public async Task<ActionResult<OfferDTO>> GetOffer(Guid bankInquiryId)
        {
            var response = await _serviceManager.LoaningService.GetOfferByInquiryId(bankInquiryId);
            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
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
