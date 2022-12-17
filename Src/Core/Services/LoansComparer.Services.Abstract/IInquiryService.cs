using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract
{
    public interface IInquiryService
    {
        //Task Add(AddInquiryDTO inquiry);

        Task<List<GetInquiryDTO>> GetAll();
    }
}
