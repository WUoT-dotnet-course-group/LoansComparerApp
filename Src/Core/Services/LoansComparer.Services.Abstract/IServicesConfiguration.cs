namespace LoansComparer.Services.Abstract
{
    public interface IServicesConfiguration
    {
        string WebClientDomain { get; }

        string GoogleAuthClientId { get; }
        string GoogleAuthSecretKey { get; }

        string EmailClientConnectionString { get; }
        string EmailClientDomain { get; }

        string GetWebClientOfferDetailsPath(string bankId, string offerId);
    }
}
