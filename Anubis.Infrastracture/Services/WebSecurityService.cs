using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application;
using Anubis.Application.Services;
using Anubis.Domain;
using Anubis.Domain.UsersDomain;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Anubis.Infrastracture.Services
{
    public class WebSecurityService : IWebSecurityService
    {
        private IUnitOfWork _unitOfWork;
        private AppSettings _appSettings;
        public WebSecurityService(IUnitOfWork _unitOfWork, IOptions<AppSettings> appSettings)
        {
            this._unitOfWork = _unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task ConfigureCookiesForUser(HttpContext context, string userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserId(userId);
            
            var encryptionToken = EncryptionService.GenerateJwtForUser(user, _appSettings.Secret);
            context.Response.Cookies.Append("X-Access-Token", encryptionToken,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(15),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });

            var refreshToken = new UserRefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            context.Response.Cookies.Append("X-Refresh-Token", refreshToken.Token,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
            user.RefreshToken = refreshToken;
            await _unitOfWork.UserRepository.UpdateUserAsync(user);
        }

        public async Task<UserRefreshToken> GenerateRefreshTokenForUser()
        {
            var refreshToken = new UserRefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public async Task SetRefreshToken(HttpContext context, UserRefreshToken refreshToken, User user)
        {
            context.Response.Cookies.Append("X-Refresh-Token", refreshToken.Token,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
            user.RefreshToken = refreshToken;
            await _unitOfWork.UserRepository.UpdateUserAsync(user);
        }

        public async Task SetJWT(User user, HttpContext context)
        {
            var encryptionToken = EncryptionService.GenerateJwtForUser(user, _appSettings.Secret);
            context.Response.Cookies.Append("X-Access-Token", encryptionToken,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(15),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
        }

        public async Task RemoveCookies(HttpContext context)
        {
            context.Response.Cookies.Delete("X-Access-Token");
            context.Response.Cookies.Delete("X-Refresh-Token");
        }
    }
}
