using Anubis.Application.Requests.ExternalLogin;
using Anubis.Application.Responses.Login;
using Anubis.Application.Services;
using Anubis.Application;
using Anubis.Application.Requests.Logout;
using Anubis.Application.Responses.Logout;
using FastEndpoints;
using Newtonsoft.Json;

namespace Anubis.Web.Endpoints.Authentication
{
    public class Logout : Endpoint<LogoutRequest, LogoutResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;
        private IWebSecurityService _webSecurityService;

        public Logout(ICommandDispatcher dispatcher, ILogger logger, IWebSecurityService webSecurityService)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _webSecurityService = webSecurityService;
        }

        public override void Configure()
        {
            Post("authentication/Logout");
        }

        public override async Task HandleAsync(LogoutRequest req, CancellationToken ct)
        {
            try
            {
                var logoutResponse = await _dispatcher.Dispatch<LogoutRequest, LogoutResponse>(req, ct);

                await _webSecurityService.RemoveCookies(HttpContext);

                await SendAsync(logoutResponse, 200, ct);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}