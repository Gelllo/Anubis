using Anubis.Application;
using Anubis.Application.Requests.Login;
using Anubis.Application.Requests.Register;
using Anubis.Application.Responses.Login;
using Anubis.Application.Responses.Register;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Authentication
{
    public class InternalLogin: Endpoint<LoginRequest, LoginResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public InternalLogin(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Post("authentication/login");
            Validator<LoginRequestValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            await SendAsync(await _dispatcher.Dispatch<LoginRequest, LoginResponse>(req, ct));
        }
    }
}
