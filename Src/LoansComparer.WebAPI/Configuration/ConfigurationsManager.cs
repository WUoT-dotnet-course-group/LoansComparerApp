namespace LoansComparer.WebAPI.Configuration
{
    public class ConfigurationsManager
    {
        private readonly DatabaseConfig _databaseConfig;

        public readonly IConfiguration Configuration;

        public ConfigurationsManager(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfig = Configuration.GetRequiredSection(DatabaseConfig.SectionName).Get<DatabaseConfig>();
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
    }
}
