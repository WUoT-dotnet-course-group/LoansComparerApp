using LoansComparer.Domain.Options;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using LoansComparer.Services.Abstract.LoaningServices;
using LoansComparer.Services.LoaningServices;
using Microsoft.Extensions.Options;

namespace LoansComparer.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IInquiryService> _lazyInquiryService;
        private readonly Lazy<ILoaningBank> _lazyLoaningBankService;
        private readonly Lazy<IBankApi> _lazyLecturerBankService;
        private readonly Lazy<IEmailService> _lazyEmailService;

        public ServiceManager(IRepositoryManager repositoryManager, IServicesConfiguration configuration, IHttpClientFactory clientFactory,
            IOptions<LoaningBankConfig> loaningBankConfig, IOptions<LecturerBankConfig> lecturerBankConfig)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, configuration));
            _lazyInquiryService = new Lazy<IInquiryService>(() => new InquiryService(repositoryManager));
            _lazyEmailService = new Lazy<IEmailService>(() => new EmailService(configuration.EmailClientConnectionString, configuration.EmailClientDomain, repositoryManager, configuration));
            _lazyLoaningBankService = new Lazy<ILoaningBank>(() => new LoaningBankService(clientFactory, loaningBankConfig));
            _lazyLecturerBankService = new Lazy<IBankApi>(() => new LecturerBankService(clientFactory, lecturerBankConfig));
        }

        public IUserService UserService => _lazyUserService.Value;

        public IInquiryService InquiryService => _lazyInquiryService.Value;

        public IEmailService EmailService => _lazyEmailService.Value;

        public ILoaningBank LoaningBankService => _lazyLoaningBankService.Value;

        public IBankApi LecturerBankService => _lazyLecturerBankService.Value;
    }
}
