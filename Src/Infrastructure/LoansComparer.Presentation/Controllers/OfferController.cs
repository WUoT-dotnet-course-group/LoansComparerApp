using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Authorize(Roles = "Bank employee")]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoaningManager _loaningManager;

        public OfferController(IServiceManager serviceManager, ILoaningManager loaningManager)
        {
            _serviceManager = serviceManager;
            _loaningManager = loaningManager;
        }

        [AllowAnonymous]
        [HttpGet("{bankId}/{offerId}")]
        public async Task<ActionResult<OfferDTO>> GetOffer(string bankId, string offerId)
        {
            var response = await _loaningManager.GetOffer(bankId, offerId);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OfferDTO>>> Get([FromQuery] PagingParameter pagingParams)
        {
            var response = await _loaningManager.LoaningBankService.GetBankOffers(pagingParams);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [HttpPatch("{offerId}/accept")]
        public async Task<ActionResult> AcceptOffer(string offerId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var user = await _serviceManager.UserService.GetData(Guid.Parse(userId));

            var response = await _loaningManager.LoaningBankService.AcceptOffer(Guid.Parse(offerId), $"{user!.FirstName} {user!.LastName}");

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            await _serviceManager.EmailService.SendAfterDecisionEmail(offerId);

            return Ok();
        }

        [HttpPatch("{offerId}/reject")]
        public async Task<ActionResult> RejectOffer(string offerId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var user = await _serviceManager.UserService.GetData(Guid.Parse(userId));
            
            var response = await _loaningManager.LoaningBankService.RejectOffer(Guid.Parse(offerId), $"{user!.FirstName} {user!.LastName}");

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            await _serviceManager.EmailService.SendAfterDecisionEmail(offerId);

            return Ok();
        }
    }
}
