namespace SomeBlog.Application.DataTransferObjects.Account
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
