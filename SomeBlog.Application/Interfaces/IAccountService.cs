using SomeBlog.Application.DataTransferObjects.Account;
using SomeBlog.Application.Wrappers;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<AuthenticationResult> LoginAsync(LoginRequest loginRequest);
        public Task<AuthenticationResult> RegisterAsync(RegisterRequest registerRequest);
        public Task<AuthenticationResult> ConfirmEmailAsync(ConfirmEmailRequest confirmEmailRequest);
        public Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        public Task ForgotPassword(ForgotPasswordRequest model);
        public Task<AuthenticationResult> ResetPassword(ResetPasswordRequest model);
    }
}
