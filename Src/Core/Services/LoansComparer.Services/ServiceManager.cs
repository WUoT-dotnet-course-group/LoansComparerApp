using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IInquiryService> _lazyInquiryService;
        private readonly Lazy<ILoaningService> _lazyLoaningService;
        private readonly Lazy<IEmailService> _lazyEmailService;

        public ServiceManager(IRepositoryManager repositoryManager, IServicesConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, configuration));
            _lazyInquiryService = new Lazy<IInquiryService>(() => new InquiryService(repositoryManager));
            _lazyLoaningService = new Lazy<ILoaningService>(() => new LoaningService(clientFactory));
            _lazyEmailService = new Lazy<IEmailService>(() => new EmailService(configuration.EmailClientConnectionString, configuration.EmailClientDomain, repositoryManager, configuration));
        }

        public IUserService UserService => _lazyUserService.Value;

        public IInquiryService InquiryService => _lazyInquiryService.Value;

        public ILoaningService LoaningService => _lazyLoaningService.Value;

        public IEmailService EmailService => _lazyEmailService.Value;
    }
}
