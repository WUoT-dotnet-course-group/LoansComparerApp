using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract
{
    public interface IInquiryService
    {
        Task Add(AddInquiryDTO inquiry);

        Task ChooseOffer(Guid inquiryId, ChooseOfferDTO chosenOffer);

        Task<List<GetInquiryDTO>> GetAllByUser(Guid userId);
    }
}
