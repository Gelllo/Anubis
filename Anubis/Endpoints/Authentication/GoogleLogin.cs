using Anubis.Application;
using Anubis.Application.Requests.ExternalLogin;
using Anubis.Application.Requests.Login;
using Anubis.Application.Requests.Register;
using Anubis.Application.Responses.Login;
using Anubis.Application.Responses.Register;
using Anubis.Application.Services;
using Anubis.Domain;
using Anubis.Infrastracture.Services;
using Anubis.Infrastracture;
using Anubis.Web.Shared;
using FastEndpoints;
using Microsoft.Extensions.Options;

namespace Anubis.Web.Endpoints.Authentication
{
    public class GoogleLogin : Endpoint<GoogleLoginRequest, LoginResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;
        private IWebSecurityService _webSecurityService;
        private IUnitOfWork _unitOfWork;

        public GoogleLogin(ICommandDispatcher dispatcher, ILogger logger, IWebSecurityService webSecurityService, IUnitOfWork unitOfWork)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _webSecurityService = webSecurityService;
            _unitOfWork = unitOfWork;
        }

        public override void Configure()
        {
            Post("authentication/GoogleLogin");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GoogleLoginRequest req, CancellationToken ct)
        {
            try
            {
                var loginResponse = await _dispatcher.Dispatch<GoogleLoginRequest, LoginResponse>(req, ct);

                var user = await _unitOfWork.UserRepository.GetUserByUserId(loginResponse.UserID);

                await _webSecurityService.SetJWT(user, HttpContext);

                var refreshToken = await _webSecurityService.GenerateRefreshTokenForUser();

                await _webSecurityService.SetRefreshToken(HttpContext, refreshToken, user);

                await SendAsync(loginResponse, 200, ct);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}