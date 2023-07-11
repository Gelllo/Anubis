using FastEndpoints;
using Anubis.Application;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;

namespace Anubis.Web.Endpoints.Users
{
    public class CreateUser:Endpoint<CreateUserRequest, CreateUserResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public CreateUser(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Post("/users/");
            Roles("ADMIN");
        }

        public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
        {
            try
            {
                await SendAsync(await _dispatcher.Dispatch<CreateUserRequest, CreateUserResponse>(req, ct));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
