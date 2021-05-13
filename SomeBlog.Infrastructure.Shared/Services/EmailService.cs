using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SomeBlog.Application.DataTransferObjects.Email;
using SomeBlog.Application.Interfaces;
using SomeBlog.Domain.Settings;
using System.Threading.Tasks;

namespace SomeBlog.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public SendGridOptions Options { get; set; }

        public EmailService(IOptions<SendGridOptions> options)
        {
            Options = options.Value;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            await Execute(Options.ApiKey, emailRequest.Subject, emailRequest.Message, emailRequest.Email);
        }

        private async Task<Response> Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            msg.SetOpenTracking(false);
            msg.SetGoogleAnalytics(false);
            msg.SetSubscriptionTracking(false);

            return await client.SendEmailAsync(msg);
        }
    }
}
