using SomeBlog.Application.DataTransferObjects.Account;
using SomeBlog.Application.Wrappers;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<AuthenticationResult> LoginAsync(string email, string password);
        public Task<AuthenticationResult> RegisterAsync(string email, string password);
        public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        public Task ForgotPassword(ForgotPasswordRequest model, string origin);
        public Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}
