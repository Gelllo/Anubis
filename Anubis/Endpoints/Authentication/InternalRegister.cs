using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using Anubis.Application.Requests.Register;
using Anubis.Application.Responses.Register;
using Anubis.Web.Shared;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Authentication
{
    public class InternalRegister : Endpoint<RegisterRequest, RegisterResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public InternalRegister(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Post("authentication/register");
            Validator<RegisterRequestValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            try
            {
                await SendAsync(await _dispatcher.Dispatch<RegisterRequest, RegisterResponse>(req, ct));
            }
            catch (Exception ex)
            {
                ThrowError("Invalid register", StatusCodes.Status400BadRequest);
                _logger.Error(ex.Message);
            }
        }
    }
}
