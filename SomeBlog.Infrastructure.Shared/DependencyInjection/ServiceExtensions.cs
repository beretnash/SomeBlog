using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SomeBlog.Application.Interfaces;
using SomeBlog.Domain.Settings;
using SomeBlog.Infrastructure.Shared.Services;

namespace SomeBlog.Infrastructure.Shared.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void AddSendGridEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = configuration["ExternalProviders:SendGrid:SenderName"];
            });
        }
    }
}
