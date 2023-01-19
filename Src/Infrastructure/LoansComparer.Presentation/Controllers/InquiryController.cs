using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Authorize(Roles = "Debtor")]
    [Route("api/inquiries")]
    public class InquiryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public InquiryController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetInquiryDTO>>> Get([FromQuery] PagingParameter pagingParams)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var inquiries = await _serviceManager.InquiryService.GetByUser(Guid.Parse(userId), pagingParams);

            return Ok(inquiries);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<CreateInquiryResponseDTO>> Create([FromBody] CreateInquiryDTO inquiry)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var inquiryId = await _serviceManager.InquiryService.Add(inquiry, userId);

            var response = await _serviceManager.LoaningService.Inquire(inquiry);
            if (response.IsSuccessful)
            {
                return Ok(new CreateInquiryResponseDTO() { InquiryId = inquiryId, BankInquiryId = response.Content!.InquiryId });
            }

            return StatusCode(500);
        }

        [AllowAnonymous]
        [HttpPatch("{inquiryId}/choose-offer")]
        public async Task<ActionResult> ChooseOffer(Guid inquiryId, [FromBody] ChooseOfferDTO chosenOffer)
        {
            await _serviceManager.InquiryService.ChooseOffer(inquiryId, chosenOffer);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("fetch-offer/{bankInquiryId}")]
        public async Task<ActionResult<OfferDTO>> FetchOffer(Guid bankInquiryId)
        {
            var response = await _serviceManager.LoaningService.FetchOffer(bankInquiryId);
            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [AllowAnonymous]
        [HttpPost("{inquiryId}/upload")]
        public async Task<ActionResult> UploadFile(Guid inquiryId)
        {
            var offerId = await _serviceManager.InquiryService.GetOfferId(inquiryId);

            var file = Request.Form.Files[0];
            var response = await _serviceManager.LoaningService.UploadFile(offerId, file.OpenReadStream(), file.FileName);
            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            await _serviceManager.EmailService.SendAfterSubmissionEmail(inquiryId);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetInquiriesAmount()
        {
            return Ok(await _serviceManager.InquiryService.GetInquiriesAmount());
        }
    }
}
