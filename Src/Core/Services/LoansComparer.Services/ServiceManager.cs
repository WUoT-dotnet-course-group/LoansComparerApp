using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IInquiryService> _lazyInquiryService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyInquiryService = new Lazy<IInquiryService>(() => new InquiryService(repositoryManager));
        }

        public IInquiryService InquiryService => _lazyInquiryService.Value;
    }
}
