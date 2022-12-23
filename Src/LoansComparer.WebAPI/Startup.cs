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

            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie(x => x.Cookie.Name = "token")
                .AddJwtBearer(x =>
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
                            context.Token = context.Request.Cookies["token"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

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
