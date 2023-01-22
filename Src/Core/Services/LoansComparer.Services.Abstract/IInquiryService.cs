using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract
{
    public interface IInquiryService
    {
        Task<Guid> Add(CreateInquiryDTO inquiry, string? userId);

        Task ChooseOffer(Guid inquiryId, ChooseOfferDTO chosenOffer);

        Task<PaginatedResponse<GetInquiryDTO>> GetByUser(Guid userId, PagingParameter pagingParams);

        Task<ChooseOfferDTO> GetOfferIds(Guid inquiryId);

        Task<int> GetInquiriesAmount();
    }
}
