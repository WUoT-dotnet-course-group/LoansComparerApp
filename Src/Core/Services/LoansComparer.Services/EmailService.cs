using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Utils;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;

namespace LoansComparer.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        private readonly string _sendOutEmailAddress;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoaningManager _loaningManager;
        private readonly IServicesConfiguration _configuration;

        public EmailService(string connectionString, string sendOutEmail, IRepositoryManager repositoryManager, ILoaningManager loaningManager, IServicesConfiguration configuration)
        {
            _emailClient = new EmailClient(connectionString);
            _sendOutEmailAddress = sendOutEmail;
            _repositoryManager = repositoryManager;
            _loaningManager = loaningManager;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string emailSubject, string emailBodyTemplate, EmailWithLinkData emailData)
            => await SendEmailsAsync(emailSubject, emailBodyTemplate, new List<EmailWithLinkData>() { emailData });

        public async Task SendEmailsAsync(string emailSubject, string emailBodyTemplate, IEnumerable<EmailWithLinkData> emailsData)
        {
            foreach (var emailData in emailsData)
            {
                var emailBody = emailBodyTemplate
                    .Replace("###NAME###", emailData.Name)
                    .Replace("###LINK###", emailData.Link);

                var emailContent = new EmailContent(emailSubject)
                {
                    PlainText = emailBody
                };

                var emailRecipients = new EmailRecipients(new List<EmailAddress>() { new EmailAddress(emailData.Email) });

                var emailMessage = new EmailMessage(_sendOutEmailAddress, emailContent, emailRecipients);

                try
                {
                    await _emailClient.SendAsync(emailMessage);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task SendAfterSubmissionEmail(Guid inquiryId)
        {
            var inquiry = await _repositoryManager.InquiryRepository.GetById(inquiryId);

            var emailData = new EmailWithLinkData
            {
                Email = inquiry.User.Email!,
                Name = inquiry.User.PersonalData!.FirstName,
                Link = _configuration.GetWebClientOfferDetailsPath(inquiry.ChosenBankId!, inquiry.ChosenOfferId!),
            };

            await SendEmailAsync(Resources.InquirySubmittedEmailSubject, Resources.InquirySubmittedEmailBody, emailData);
        }

        public async Task SendAfterDecisionEmail(string offerId)
        {
            var loaningBankId = _loaningManager.LoaningBankService.Id;

            var debtor = await _repositoryManager.InquiryRepository.GetDebtorByOffer(loaningBankId, offerId);

            var emailData = new EmailWithLinkData
            {
                Email = debtor.Email!,
                Name = debtor.PersonalData!.FirstName,
                Link = _configuration.GetWebClientOfferDetailsPath(loaningBankId, offerId),
            };

            await SendEmailAsync(Resources.OfferStatusChangedEmailSubject, Resources.OfferStatusChangedEmailBody, emailData);
        }
    }
}