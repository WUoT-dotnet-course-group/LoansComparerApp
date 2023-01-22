using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IInquiryService> _lazyInquiryService;
        private readonly Lazy<IEmailService> _lazyEmailService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoaningManager loaningManager, IServicesConfiguration configuration)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, configuration));
            _lazyInquiryService = new Lazy<IInquiryService>(() => new InquiryService(repositoryManager));
            _lazyEmailService = new Lazy<IEmailService>(() => new EmailService(configuration.EmailClientConnectionString, configuration.EmailClientDomain, 
                repositoryManager, loaningManager, configuration));
        }

        public IUserService UserService => _lazyUserService.Value;

        public IInquiryService InquiryService => _lazyInquiryService.Value;

        public IEmailService EmailService => _lazyEmailService.Value;
    }
}
