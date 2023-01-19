using LoansComparer.DataPersistence;
using LoansComparer.DataPersistence.Repositories;
using LoansComparer.Domain.Repositories;
using LoansComparer.Services.Abstract;
using LoansComparer.Services;
using LoansComparer.WebAPI.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mapster;
using MapsterMapper;
using LoansComparer.Domain.Options;

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
                conf.UseLazyLoadingProxies().UseSqlServer(ConfigurationsManager.DbConnectionString));

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServicesConfiguration, ConfigurationsManager>();

            services.Configure<LoaningBankConfig>(ConfigurationsManager.Configuration.GetSection(LoaningBankConfig.SectionName));
            services.Configure<LecturerBankConfig>(ConfigurationsManager.Configuration.GetSection(LecturerBankConfig.SectionName));

            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(typeof(Services.Mapping.AssemblyReference).Assembly);
            services.AddSingleton(mappingConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddHttpClient("LoaningBank", client =>
            {
                client.BaseAddress = new Uri(ConfigurationsManager.LoaningBankDomain);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient("LecturerBank", client =>
            {
                client.BaseAddress = new Uri(ConfigurationsManager.LecturerBankDomain);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200", "https://loans-comparer.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationsManager.GoogleAuthSecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Headers.TryGetValue("Authentication", out var token))
                            {
                                context.Token = token.FirstOrDefault();
                            }
                            return Task.CompletedTask;
                        }
                    };
                });


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

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
