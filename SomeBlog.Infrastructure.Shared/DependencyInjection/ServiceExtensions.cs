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

            services.Configure<SendGridOptions>(options =>
            {
                options.ApiKey = configuration["SendGridOptions:ApiKey"];
                options.SenderEmail = configuration["SendGridOptions:SenderEmail"];
                options.SenderName = configuration["SendGridOptions:SenderName"];
            });
        }
    }
}
