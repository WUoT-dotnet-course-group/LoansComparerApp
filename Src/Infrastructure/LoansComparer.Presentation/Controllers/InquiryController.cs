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
        private readonly ILoaningManager _loaningManager;

        public InquiryController(IServiceManager serviceManager, ILoaningManager loaningManager)
        {
            _serviceManager = serviceManager;
            _loaningManager = loaningManager;
        }

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

            var response = await _loaningManager.InquireToAll(inquiry);

            if (response.Any())
            {
                return Ok(new CreateInquiryResponseDTO() { InquiryId = inquiryId, BankInquiries = response });
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPatch("{inquiryId}/choose-offer")]
        public async Task<ActionResult> ChooseOffer(Guid inquiryId, [FromBody] ChooseOfferDTO chosenOffer)
        {
            await _serviceManager.InquiryService.ChooseOffer(inquiryId, chosenOffer);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("fetch-offer/{bankId}/{bankInquiryId}")]
        public async Task<ActionResult<OfferDTO>> FetchOffer(string bankId, string bankInquiryId)
        {
            var response = await _loaningManager.FetchOffer(bankId, bankInquiryId);
            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [AllowAnonymous]
        [HttpGet("{inquiryId}/agreement")]
        public async Task<ActionResult> DownloadAgreement(Guid inquiryId)
        {
            var inquiry = await _serviceManager.InquiryService.GetOfferIds(inquiryId);

            var response = await _loaningManager.DownloadFile(inquiry.BankId, inquiry.OfferId);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return File(response.Content!, "text/plain", fileDownloadName: "arrangement.txt");
        }

        [AllowAnonymous]
        [HttpPost("{inquiryId}/upload")]
        public async Task<ActionResult> UploadFile(Guid inquiryId)
        {
            var offerIds = await _serviceManager.InquiryService.GetOfferIds(inquiryId);

            var file = Request.Form.Files[0];
            var response = await _loaningManager.UploadFile(offerIds.BankId, offerIds.OfferId, file.OpenReadStream(), file.FileName);
            if (!response.IsSuccessful 
                && response.StatusCode != System.Net.HttpStatusCode.InternalServerError) // workaround for temporary unavailable lecturer endpoint
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
