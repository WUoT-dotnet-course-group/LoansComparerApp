namespace LoansComparer.Services.Abstract
{
    public interface IServicesConfiguration
    {
        string GetGoogleAuthClientId();
        string GetGoogleAuthSecretKey();
    }
}
