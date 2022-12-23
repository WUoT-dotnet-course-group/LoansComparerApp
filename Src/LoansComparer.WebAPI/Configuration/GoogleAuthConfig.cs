namespace LoansComparer.WebAPI.Configuration
{
    internal class GoogleAuthConfig
    {
        public const string SectionName = "GoogleAuth";

        public string ClientId { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
