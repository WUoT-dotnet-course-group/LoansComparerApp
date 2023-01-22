using LoansComparer.Services.Abstract.LoaningServices;
using LoansComparer.Services.Abstract;
using LoansComparer.Domain.Options;
using LoansComparer.Services.LoaningServices;
using Microsoft.Extensions.Options;
using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;

namespace LoansComparer.Services
{
    public sealed class LoaningManager : ILoaningManager
    {
        private readonly Dictionary<string, IBankApi> BankServices;

        public LoaningManager(IHttpClientFactory clientFactory, IOptions<LoaningBankConfig> loaningBankConfig, IOptions<LecturerBankConfig> lecturerBankConfig)
        {
            LoaningBankService = new LoaningBankService(clientFactory, loaningBankConfig);
            LecturerBankService = new LecturerBankService(clientFactory, lecturerBankConfig);

            BankServices = new()
            {
                { LoaningBankService.Id,  LoaningBankService },
                { LecturerBankService.Id, LecturerBankService }
            };
        }

        public ILoaningBank LoaningBankService { get; private set; }

        public IBankApi LecturerBankService { get; private set; }

        public async Task<Dictionary<string, string>> InquireToAll(CreateInquiryDTO inquiry)
        {
            var bankInquiries = new Dictionary<string, string>();

            var response = await LecturerBankService.Inquire(inquiry);
            if (response.IsSuccessful)
            {
                bankInquiries.Add(LecturerBankService.Id, response.Content!.InquiryId);
            }

            response = await LoaningBankService.Inquire(inquiry);
            if (response.IsSuccessful)
            {
                bankInquiries.Add(LoaningBankService.Id, response.Content!.InquiryId);
            }

            return bankInquiries;
        }

        public async Task<BaseResponse<OfferDTO>> FetchOffer(string bankId, string bankInquiryId)
            => await BankServices[bankId].FetchOffer(bankInquiryId);

        public async Task<BaseResponse> UploadFile(string bankId, string offerId, Stream fileStream, string filename)
            => await BankServices[bankId].UploadFile(offerId, fileStream, filename);

        public async Task<BaseResponse<OfferDTO>> GetOffer(string bankId, string offerId)
            => await BankServices[bankId].GetOffer(offerId);

        public async Task<BaseResponse<Stream>> DownloadFile(string bankId, string offerId)
        {
            var offerResponse = await GetOffer(bankId, offerId);

            return new BaseResponse<Stream>
            {
                IsSuccessful = offerResponse.IsSuccessful,
                StatusCode = offerResponse.StatusCode,
                Content = await BankServices[bankId].DownloadFile(offerResponse.Content!.DocumentLink!)
            };
        }
    }
}
