using LoansComparer.Domain.Options;
using LoansComparer.Services.Abstract;

namespace LoansComparer.WebAPI.Configuration
{
    public class ConfigurationsManager : IServicesConfiguration
    {
        private readonly DatabaseConfig _databaseConfig;
        private readonly GoogleAuthConfig _googleAuthConfig;
        private readonly EmailServiceConfig _emailServiceConfig;

        private readonly LoaningBankConfig _loaningBankConfig;
        private readonly LecturerBankConfig _lecturerBankConfig;
        private readonly OtherTeamBankConfig _otherTeamBankConfig;

        public readonly IConfiguration Configuration;

        public ConfigurationsManager(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfig = Configuration.GetRequiredSection(DatabaseConfig.SectionName).Get<DatabaseConfig>();
            _googleAuthConfig = Configuration.GetRequiredSection(GoogleAuthConfig.SectionName).Get<GoogleAuthConfig>();
            _emailServiceConfig = Configuration.GetRequiredSection(EmailServiceConfig.SectionName).Get<EmailServiceConfig>();

            _loaningBankConfig = Configuration.GetRequiredSection(LoaningBankConfig.SectionName).Get<LoaningBankConfig>();
            _lecturerBankConfig = Configuration.GetRequiredSection(LecturerBankConfig.SectionName).Get<LecturerBankConfig>();
            _otherTeamBankConfig = Configuration.GetRequiredSection(OtherTeamBankConfig.SectionName).Get<OtherTeamBankConfig>();
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

        public string LoaningBankDomain => _loaningBankConfig.Domain;

        public string LecturerBankDomain => _lecturerBankConfig.Domain;

        public string OtherTeamBankDomain => _otherTeamBankConfig.Domain;

        public string GoogleAuthClientId => _googleAuthConfig.ClientId;

        public string GoogleAuthSecretKey => _googleAuthConfig.SecretKey;

        public string WebClientDomain => Configuration.GetValue<string>("WebClientDomain");

        public string GetWebClientOfferDetailsPath(string bankId, string offerId) => WebClientDomain + $"offers/{bankId}/{offerId}";
    }
}
