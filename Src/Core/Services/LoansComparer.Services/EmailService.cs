using Azure.Communication.Email;
using Azure.Communication.Email.Models;
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

        public async Task SendAfterSubmissionEmail(Guid inquiryId)
        {
            var inquiry = await _repositoryManager.InquiryRepository.GetById(inquiryId);

            var emailBody = string.Format(Resources.InquirySubmittedEmailBody, inquiry.User.PersonalData!.FirstName,
                _configuration.GetWebClientOfferDetailsPath(inquiry.ChosenBankId!, inquiry.ChosenOfferId!));

            await SendEmailAsync(Resources.InquirySubmittedEmailSubject, emailBody, inquiry.User.Email!);
        }

        public async Task SendAfterDecisionEmail(string offerId)
        {
            var loaningBankId = _loaningManager.LoaningBankService.Id;

            var debtor = await _repositoryManager.InquiryRepository.GetDebtorByOffer(loaningBankId, offerId);

            var emailBody = string.Format(Resources.OfferStatusChangedEmailBody, debtor.PersonalData!.FirstName,
                _configuration.GetWebClientOfferDetailsPath(loaningBankId, offerId));

            await SendEmailAsync(Resources.OfferStatusChangedEmailSubject, emailBody, debtor.Email!);
        }
    }
}