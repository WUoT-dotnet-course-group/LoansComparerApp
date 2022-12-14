using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
//using Mapster;

namespace LoansComparer.Services
{
    internal sealed class InquiryService : IInquiryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public InquiryService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

       /* public async Task Add(AddInquiryDTO inquiry)
        {
            var inquiryToAdd = inquiry.Adapt<Inquiry>();

            await _repositoryManager.InquiryRepository.Add(inquiryToAdd);
        }

        public async Task<List<GetInquiryDTO>> GetAll()
        {
            var inquiries = await _repositoryManager.InquiryRepository.GetAll();

            return inquiries.Adapt<List<GetInquiryDTO>>();
        }*/
    }
}
