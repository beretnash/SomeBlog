using Microsoft.AspNetCore.Mvc;
using SomeBlog.Application.DataTransferObjects.Account;
using SomeBlog.Application.Interfaces;
using System.Threading.Tasks;

namespace SomeBlog.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            return Ok(await _accountService.LoginAsync(request));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            return Ok(await _accountService.RefreshTokenAsync(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            return Ok(await _accountService.RegisterAsync(request));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailRequest confirmEmailRequest)
        {
            return Ok(await _accountService.ConfirmEmailAsync(confirmEmailRequest));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            return Ok(await _accountService.ResetPassword(model));
        }
    }
}
