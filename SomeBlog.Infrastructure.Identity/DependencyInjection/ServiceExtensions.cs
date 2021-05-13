using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SomeBlog.Application.Interfaces;
using SomeBlog.Domain.Settings;
using SomeBlog.Infrastructure.Identity.Models;
using SomeBlog.Infrastructure.Identity.Services;
using System.Text;

namespace SomeBlog.Infrastructure.Identity.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddScoped<IAccountService, AccountService>();
        }

        public static void AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(key: nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(configureOptions: t =>
            {
                t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                t.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; ;
                t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}
