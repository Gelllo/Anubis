using Anubis.Application.Requests.Register;
using Anubis.Application.Responses.Register;
using Anubis.Application;
using Anubis.Application.Requests.Token;
using Anubis.Application.Responses.Token;
using Anubis.Application.Services;
using Anubis.Infrastructure.Services;
using Anubis.Web.Shared;
using Azure.Core;
using FastEndpoints;
using Anubis.Domain;
using Anubis.Infrastructure;
using Microsoft.Extensions.Options;

namespace Anubis.Web.Endpoints.Authentication
{
    public class RefreshToken : Endpoint<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly ILogger _logger;
        private readonly IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IWebSecurityService _webSecurityService;

        public RefreshToken(ILogger logger, IAnubisUnitOfWork<AnubisContext> unitOfWork, IOptions<AppSettings> appSettings, IWebSecurityService webSecurityService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _appSettings = appSettings.Value;
            _webSecurityService = webSecurityService;
        }

        public override void Configure()
        {
            Post("authentication/refreshToken");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
        {
            var refreshToken = HttpContext.Request.Cookies["X-Refresh-Token"];

            var user = await _unitOfWork.UserRepository.GetUserByToken(refreshToken);

            if (user == null || user.RefreshToken.Expires < DateTime.Now)
            {
                ThrowError(ErrorMessages.EXPIRED_TOKEN,StatusCodes.Status401Unauthorized);
            }

            this._webSecurityService.SetJWT(user, HttpContext);

            await SendOkAsync();
        }
    }
}
