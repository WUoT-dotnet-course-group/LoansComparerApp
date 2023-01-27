using LoansComparer.CrossCutting.DTO;

namespace LoansComparer.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailSubject, string emailBodyTemplate, EmailWithLinkData emailData);
        Task SendEmailsAsync(string emailSubject, string emailBodyTemplate, IEnumerable<EmailWithLinkData> emailsData);
        Task SendAfterSubmissionEmail(Guid inquiryId);
        Task SendAfterDecisionEmail(string offerId);
    }
}
