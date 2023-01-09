using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IInquiryService> _lazyInquiryService;
        private readonly Lazy<ILoaningService> _lazyLoaningService;

        public ServiceManager(IRepositoryManager repositoryManager, IServicesConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, configuration));
            _lazyInquiryService = new Lazy<IInquiryService>(() => new InquiryService(repositoryManager));
            _lazyLoaningService = new Lazy<ILoaningService>(() => new LoaningService(clientFactory));
        }

        public IUserService UserService => _lazyUserService.Value;

        public IInquiryService InquiryService => _lazyInquiryService.Value;

        public ILoaningService LoaningService => _lazyLoaningService.Value;
    }
}
