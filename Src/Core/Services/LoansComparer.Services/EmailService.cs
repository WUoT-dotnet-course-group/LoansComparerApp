using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        private readonly string _sendOutEmailAddress;

        public EmailService(string connectionString, string sendOutEmail)
        {
            _emailClient = new EmailClient(connectionString);
            _sendOutEmailAddress = sendOutEmail;
        }

        public async Task SendEmailAsync(string emailSubject, string emailBody, string emailAddress)
            => await SendEmailsAsync(emailSubject, emailBody, new List<string> { emailAddress });

        public async Task SendEmailsAsync(string emailSubject, string emailBody, List<string> emailAddresses)
        {
            var emailContent = new EmailContent(emailSubject)
            {
                PlainText = emailBody
            };

            var emailRecipients = new EmailRecipients(emailAddresses.Select(x => new EmailAddress(x)));

            var emailMessage = new EmailMessage(_sendOutEmailAddress, emailContent, emailRecipients);

            await _emailClient.SendAsync(emailMessage);
        }
    }
}