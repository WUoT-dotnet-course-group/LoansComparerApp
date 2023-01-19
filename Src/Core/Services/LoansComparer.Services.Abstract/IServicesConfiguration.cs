namespace LoansComparer.Services.Abstract
{
    public interface IServicesConfiguration
    {
        string GoogleAuthClientId { get; }
        string GoogleAuthSecretKey { get; }

        string EmailClientConnectionString { get; }
        string EmailClientDomain { get; }

        KeyValuePair<string, string> LoaningBankClientCredentials { get; }

        string GetWebClientOfferDetailsPath(Guid offerId);
    }
}
