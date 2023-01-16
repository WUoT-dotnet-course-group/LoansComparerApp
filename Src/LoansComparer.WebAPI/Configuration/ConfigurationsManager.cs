using LoansComparer.Services.Abstract;

namespace LoansComparer.WebAPI.Configuration
{
    public class ConfigurationsManager : IServicesConfiguration
    {
        private readonly DatabaseConfig _databaseConfig;
        private readonly GoogleAuthConfig _googleAuthConfig;
        private readonly EmailServiceConfig _emailServiceConfig;

        public readonly IConfiguration Configuration;

        public ConfigurationsManager(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfig = Configuration.GetRequiredSection(DatabaseConfig.SectionName).Get<DatabaseConfig>();
            _googleAuthConfig = Configuration.GetRequiredSection(GoogleAuthConfig.SectionName).Get<GoogleAuthConfig>();
            _emailServiceConfig = Configuration.GetRequiredSection(EmailServiceConfig.SectionName).Get<EmailServiceConfig>();
        }

        public string DbConnectionString
        {
            get
            {
                return $"Server={_databaseConfig.Server};Initial Catalog={_databaseConfig.DbName};" +
                    $"Persist Security Info=False;" +
                    $"User ID={_databaseConfig.Login};Password={_databaseConfig.Password};" +
                    $"MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=15;";
            }
        }

        public string EmailClientConnectionString
        {
            get
            {
                return $"endpoint={_emailServiceConfig.Endpoint};accesskey={_emailServiceConfig.AccessKey}";
            }
        }

        public string EmailClientDomain => _emailServiceConfig.Domain;

        public string LoaningBankDomain => Configuration.GetValue<string>("LoaningBankDomain");

        public string GoogleAuthClientId => _googleAuthConfig.ClientId;

        public string GoogleAuthSecretKey => _googleAuthConfig.SecretKey;
    }
}
