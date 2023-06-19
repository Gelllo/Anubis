using FastEndpoints;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;

namespace Anubis.Web.Endpoints.Users
{
    public class UpdateUser : Endpoint<UpdateUserRequest, UpdateUserResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public UpdateUser(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Put("/users/");
            Roles("Admin");
        }

        public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        {
            await SendAsync(await _dispatcher.Dispatch<UpdateUserRequest, UpdateUserResponse>(req, ct));
        }
    }
}
