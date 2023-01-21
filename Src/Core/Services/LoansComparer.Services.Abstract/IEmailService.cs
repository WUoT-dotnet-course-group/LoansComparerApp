namespace LoansComparer.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailSubject, string emailBody, string emailAddress);
        Task SendEmailsAsync(string emailSubject, string emailBody, List<string> emailAddresses);
        Task SendAfterSubmissionEmail(Guid inquiryId);
        Task SendAfterDecisionEmail(string offerId);
    }
}
