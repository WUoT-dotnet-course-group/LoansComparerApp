using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IInquiryService> _lazyInquiryService;

        public ServiceManager(IRepositoryManager repositoryManager, IServicesConfiguration configuration)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, configuration));
            _lazyInquiryService = new Lazy<IInquiryService>(() => new InquiryService(repositoryManager));
        }

        public IUserService UserService => _lazyUserService.Value;

        public IInquiryService InquiryService => _lazyInquiryService.Value;
    }
}
