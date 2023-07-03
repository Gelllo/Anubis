using Anubis.Domain.UsersDomain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Services
{
    public interface IWebSecurityService
    {
        public Task<UserRefreshToken> GenerateRefreshTokenForUser();
        public Task ConfigureCookiesForUser(HttpContext context, string? userId);
        public Task SetRefreshToken(HttpContext context, UserRefreshToken refreshToken, User user);
        public Task SetJWT(User user, HttpContext context);
        public Task RemoveCookies(HttpContext context);
    }
}
