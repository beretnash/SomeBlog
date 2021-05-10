using SomeBlog.Application.Wrappers;
using System.Collections.Generic;

namespace SomeBlog.Application.DataTransferObjects.Account
{
    public class AuthenticationResult : Response<string>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
