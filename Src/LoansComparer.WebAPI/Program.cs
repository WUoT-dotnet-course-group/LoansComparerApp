using LoansComparer.DataPersistence;
using LoansComparer.DataPersistence.Utils;

namespace LoansComparer
{
    public class Program
    {
        protected Program() { }

        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            InitializeDatabase(webHost);

            await webHost.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        private static void InitializeDatabase(IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<RepositoryDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}