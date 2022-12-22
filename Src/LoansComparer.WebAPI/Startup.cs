using LoansComparer.DataPersistence;
using LoansComparer.DataPersistence.Repositories;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using LoansComparer.Services;
using LoansComparer.WebAPI.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer
{
    public class Startup
    {
        private readonly ConfigurationsManager ConfigurationsManager;

        public Startup(IConfiguration configuration)
        {
            ConfigurationsManager = new ConfigurationsManager(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositoryDbContext>(conf =>
                conf.UseSqlServer(ConfigurationsManager.DbConnectionString));

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoansComparer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
