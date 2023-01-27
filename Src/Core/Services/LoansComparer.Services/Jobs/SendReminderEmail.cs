using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Utils;
using LoansComparer.Services.Abstract;
using Quartz;

namespace LoansComparer.Services.Jobs
{
    public class SendReminderEmail : IJob
    {
        private readonly IServiceManager _serviceManager;
        private string WebClientDomain { get; }

        public SendReminderEmail(IServiceManager serviceManager, IServicesConfiguration configuration)
        {
            _serviceManager = serviceManager;
            WebClientDomain = configuration.WebClientDomain;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var baseEmailData = await _serviceManager.UserService.GetDataForEmailReminder();
            var emailData = baseEmailData.Select(x => new EmailWithLinkData
            {
                Email = x.Email,
                Name = x.Name,
                Link = WebClientDomain
            });

            await _serviceManager.EmailService.SendEmailsAsync(Resources.DailyReminderEmailSubject, Resources.DailyReminderEmailBody, emailData);
        }
    }
}
