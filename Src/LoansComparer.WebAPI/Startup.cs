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
            services.AddScoped<IServicesConfiguration, ConfigurationsManager>();

            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(typeof(Services.Mapping.AssemblyReference).Assembly);
            services.AddSingleton(mappingConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddHttpClient("LoaningBank", client =>
            {
                client.BaseAddress = new Uri(ConfigurationsManager.LoaningBankDomain);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200", "https://loans-comparer.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationsManager.GetGoogleAuthSecretKey())),
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
