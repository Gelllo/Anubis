using Anubis.Application;
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
    public class InternalLogin: Endpoint<LoginRequest, LoginResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;
        private IWebSecurityService _webSecurityService;
        private IUnitOfWork _unitOfWork;
        public InternalLogin(ICommandDispatcher dispatcher, ILogger logger, IWebSecurityService securityService, IUnitOfWork unitOfWork)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _webSecurityService = securityService;
            _unitOfWork = unitOfWork;
        }

        public override void Configure()
        {
            Post("authentication/login");
            Validator<LoginRequestValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            try
            {
                var loginResponse = await _dispatcher.Dispatch<LoginRequest, LoginResponse>(req, ct);

                var user = await _unitOfWork.UserRepository.GetUserByUserId(loginResponse.UserID);

                await _webSecurityService.SetJWT(user, HttpContext);

                var refreshToken = await _webSecurityService.GenerateRefreshTokenForUser();

                await _webSecurityService.SetRefreshToken(HttpContext, refreshToken, user);

                await SendAsync(loginResponse, 200, ct);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(ErrorMessages.INVALID_CREDENTIALS))
                {
                    ThrowError(ErrorMessages.INVALID_CREDENTIALS);
                }

                _logger.Error(ex.Message);
            }
        }
    }
}
