using Anubis.Application;
using Anubis.Application.Requests.ExternalLogin;
using Anubis.Application.Requests.Login;
using Anubis.Application.Requests.Register;
using Anubis.Application.Responses.Login;
using Anubis.Application.Responses.Register;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Authentication
{
    public class GoogleLogin : Endpoint<GoogleLoginRequest, LoginResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public GoogleLogin(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Post("authentication/GoogleLogin");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GoogleLoginRequest req, CancellationToken ct)
        {
            await SendAsync(await _dispatcher.Dispatch<GoogleLoginRequest, LoginResponse>(req, ct));
        }
    }
}