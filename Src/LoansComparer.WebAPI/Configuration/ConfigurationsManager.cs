using LoansComparer.Services.Abstract;

namespace LoansComparer.WebAPI.Configuration
{
    public class ConfigurationsManager : IServicesConfiguration
    {
        private readonly DatabaseConfig _databaseConfig;
        private readonly GoogleAuthConfig _googleAuthConfig;

        public readonly IConfiguration Configuration;

        public ConfigurationsManager(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfig = Configuration.GetRequiredSection(DatabaseConfig.SectionName).Get<DatabaseConfig>();
            _googleAuthConfig = Configuration.GetRequiredSection(GoogleAuthConfig.SectionName).Get<GoogleAuthConfig>();
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

        public string GetGoogleAuthClientId() => _googleAuthConfig.ClientId;
        public string GetGoogleAuthSecretKey() => _googleAuthConfig.SecretKey;
    }
}
