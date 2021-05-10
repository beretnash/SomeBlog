using SomeBlog.Application.DataTransferObjects.Email;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}
