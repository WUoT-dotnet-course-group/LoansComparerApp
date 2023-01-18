using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.CrossCutting.Utils;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using Mapster;

namespace LoansComparer.Services
{
    internal sealed class InquiryService : IInquiryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        private readonly IServicesConfiguration _configuration;

        public InquiryService(IRepositoryManager repositoryManager, IServiceManager serviceManager, IServicesConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
            _configuration = configuration;
        }

        public async Task<Guid> Add(CreateInquiryDTO inquiry, string? userId)
        {
            var inquiryToAdd = inquiry.Adapt<Inquiry>();

            if (userId is null) // anonymous user
            {
                inquiryToAdd.User = new User()
                {
                    PersonalData = inquiry.PersonalData.Adapt<PersonalData>(),
                    Email = inquiry.PersonalData.Email,
                };
            }
            else
            {
                inquiryToAdd.User = await _repositoryManager.UserRepository.GetUserById(Guid.Parse(userId));
                inquiryToAdd.User.PersonalData ??= inquiry.PersonalData.Adapt<PersonalData>(); // if user has not fill personal data yet
            }

            return await _repositoryManager.InquiryRepository.Add(inquiryToAdd);
        }

        public async Task ChooseOffer(Guid inquiryId, ChooseOfferDTO chosenOffer)
        {
            var inquiry = await _repositoryManager.InquiryRepository.GetById(inquiryId);

            inquiry.Bank = await _repositoryManager.BankRepository.GetById(Guid.Empty); // as long as service handles only one bank
            //inquiry.Bank = await _repositoryManager.BankRepository.GetById(chosenOffer.BankId);
            inquiry.ChosenOfferId = chosenOffer.OfferId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<GetInquiryDTO>> GetByUser(Guid userId, PagingParameter pagingParams)
        {
            var sortOrderDesc = pagingParams.SortOrder;
            var sortHeader = pagingParams.SortHeader;

            EnumExtension.TryGetEnumValue(sortOrderDesc, out SortOrder sortOrder);

            var paginatedInquiries = await _repositoryManager.InquiryRepository
                .GetByUser<GetInquiryDTO>(userId, pagingParams.PageIndex, pagingParams.PageSize, sortOrder, sortHeader!);

            return paginatedInquiries.Adapt<PaginatedResponse<GetInquiryDTO>>();
        }

        public async Task<Guid> GetOfferId(Guid inquiryId)
        {
            var inquiry = await _repositoryManager.InquiryRepository.GetById(inquiryId);

            return inquiry.ChosenOfferId;
        }

        public async Task<int> GetInquiriesAmount() => await _repositoryManager.InquiryRepository.Count();

        public async Task SendAfterSubmissionEmail(Guid inquiryId)
        {
            var inquiry = await _repositoryManager.InquiryRepository.GetById(inquiryId);

            var emailBody = string.Format(Resources.InquirySubmittedEmailBody, inquiry.User.PersonalData!.FirstName, _configuration.GetWebClientOfferDetailsPath(inquiry.ChosenOfferId));

            await _serviceManager.EmailService.SendEmailAsync(Resources.InquirySubmittedEmailSubject, emailBody, inquiry.User.Email!);
        }
    }
}
