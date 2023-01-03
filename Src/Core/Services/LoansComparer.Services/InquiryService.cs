﻿using LoansComparer.CrossCutting.DTO;
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

        public async Task Add(AddInquiryDTO inquiry, string? userId)
        {
            var inquiryToAdd = inquiry.Adapt<Inquiry>();

            if (userId is null)
            {
                inquiryToAdd.User = new User()
                {
                    PersonalData = inquiry.PersonalData.Adapt<PersonalData>(),
                };
            }
            else
            {
                inquiryToAdd.User = await _repositoryManager.UserRepository.GetUserById(Guid.Parse(userId));
            }

            await _repositoryManager.InquiryRepository.Add(inquiryToAdd);
        }

        public async Task ChooseOffer(Guid inquiryId, ChooseOfferDTO chosenOffer)
        {
            var inquiry = await _repositoryManager.InquiryRepository.GetById(inquiryId);

            inquiry.Bank = await _repositoryManager.BankRepository.GetById(chosenOffer.OfferBankId);
            inquiry.ChosenBankInquiryId = chosenOffer.OfferId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetInquiryDTO>> GetAllByUser(Guid userId)
        {
            var inquiries = await _repositoryManager.InquiryRepository.GetAllByUser(userId);

            // TODO: configurate adapt method to fill ChosenBank property

            return inquiries.Adapt<List<GetInquiryDTO>>();
        }
    }
}
