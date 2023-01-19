using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoansComparer.Presentation.Controllers
{
    [ApiController]
    [Authorize(Roles = "Bank employee")]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public OfferController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OfferDTO>>> Get([FromQuery] PagingParameter pagingParams)
        {
            var response = await _serviceManager.LoaningService.GetBankOffers(pagingParams);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [AllowAnonymous]
        [HttpGet("{offerId}")]
        public async Task<ActionResult<OfferDTO>> GetOffer(Guid offerId)
        {
            var response = await _serviceManager.LoaningService.GetOfferById(offerId);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [HttpPatch("{offerId}/accept")]
        public async Task<ActionResult> AcceptOffer(Guid offerId)
        {
            var response = await _serviceManager.LoaningService.AcceptOffer(offerId);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            await _serviceManager.EmailService.SendAfterDecisionEmail(offerId);

            return Ok();
        }

        [HttpPatch("{offerId}/reject")]
        public async Task<ActionResult> RejectOffer(Guid offerId)
        {
            var response = await _serviceManager.LoaningService.RejectOffer(offerId);

            if (!response.IsSuccessful)
            {
                return NotFound();
            }

            await _serviceManager.EmailService.SendAfterDecisionEmail(offerId);

            return Ok();
        }
    }
}
