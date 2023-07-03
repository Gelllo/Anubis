using Anubis.Application.Requests.ExternalLogin;
using Anubis.Application.Responses.Login;
using Anubis.Application.Services;
using Anubis.Application;
using Anubis.Infrastructure;
using FastEndpoints;
using Newtonsoft.Json;

namespace Anubis.Web.Endpoints.Authentication
{
    public class FacebookLogin : Endpoint<FacebookLoginRequest, LoginResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;
        private IWebSecurityService _webSecurityService;
        private IAnubisUnitOfWork<AnubisContext> _unitOfWork;
        private readonly HttpClient _httpClient;

        public FacebookLogin(ICommandDispatcher dispatcher, ILogger logger, IWebSecurityService webSecurityService, IAnubisUnitOfWork<AnubisContext> unitOfWork, HttpClient httpClient)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _webSecurityService = webSecurityService;
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }

        public override void Configure()
        {
            Post("authentication/FacebookLogin");
            AllowAnonymous();
        }

        public override async Task HandleAsync(FacebookLoginRequest req, CancellationToken ct)
        {
            try
            {
                var loginResponse = await _dispatcher.Dispatch<FacebookLoginRequest, LoginResponse>(req, ct);

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
