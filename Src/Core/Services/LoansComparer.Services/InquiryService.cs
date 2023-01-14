using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using Mapster;

namespace LoansComparer.Services
{
    internal sealed class InquiryService : IInquiryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public InquiryService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

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
    }
}
