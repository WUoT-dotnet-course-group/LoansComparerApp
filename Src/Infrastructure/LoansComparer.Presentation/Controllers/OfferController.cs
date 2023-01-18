using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
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
            var offer = await _serviceManager.LoaningService.GetOfferById(offerId);

            if (!offer.IsSuccessful)
            {
                return NotFound();
            }

            return Ok(offer.Content);
        }
    }
}
