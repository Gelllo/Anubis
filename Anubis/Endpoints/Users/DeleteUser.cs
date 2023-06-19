using FastEndpoints;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;

namespace Anubis.Web.Endpoints.Users
{
    public class DeleteUser : Endpoint<DeleteUserRequest, DeleteUserResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public DeleteUser(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Delete("/users/");
            Roles("Admin");
        }

        public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        {
            await SendAsync(await _dispatcher.Dispatch<DeleteUserRequest, DeleteUserResponse>(req, ct));
        }
    }
}
