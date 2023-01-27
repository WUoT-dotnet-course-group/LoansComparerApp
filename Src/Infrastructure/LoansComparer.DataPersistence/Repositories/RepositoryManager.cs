using LoansComparer.Domain.Repositories;

namespace LoansComparer.DataPersistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IInquiryRepository> _inquiryRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            _inquiryRepository = new Lazy<IInquiryRepository>(() => new InquiryRepository(dbContext));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IUserRepository UserRepository => _userRepository.Value;
        public IInquiryRepository InquiryRepository => _inquiryRepository.Value;
        public IUnitOfWork UnitOfWork => _unitOfWork.Value;
    }
}
