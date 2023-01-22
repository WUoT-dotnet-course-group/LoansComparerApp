using LoansComparer.CrossCutting.DTO.LoaningBank;
using LoansComparer.CrossCutting.DTO;
using LoansComparer.Services.Abstract.LoaningServices;

namespace LoansComparer.Services.Abstract
{
    public interface ILoaningManager
    {
        ILoaningBank LoaningBankService { get; }
        IBankApi LecturerBankService { get; }

        Task<Dictionary<string, string>> InquireToAll(CreateInquiryDTO inquiry);
        Task<BaseResponse<OfferDTO>> FetchOffer(string bankId, string bankInquiryId);
        Task<BaseResponse> UploadFile(string bankId, string offerId, Stream fileStream, string filename);
        Task<BaseResponse<OfferDTO>> GetOffer(string bankId, string offerId);
    }
}
