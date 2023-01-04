using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    internal sealed class LoaningService : ILoaningService
    {
        private readonly IRepositoryManager _repositoryManager;

        public LoaningService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
    }
}
