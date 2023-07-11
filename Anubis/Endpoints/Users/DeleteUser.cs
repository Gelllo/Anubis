using FastEndpoints;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;

namespace Anubis.Web.Endpoints.Users
{
    public class DeleteUser : EndpointWithoutRequest
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
            Delete("/users/{ID}");
            Roles("ADMIN");
        }

        public override async Task HandleAsync( CancellationToken ct)
        {
            try
            {
                var req = new DeleteUserRequest() { Id = Route<int>("ID") };
                await SendAsync(await _dispatcher.Dispatch<DeleteUserRequest, DeleteUserResponse>(req, ct));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            
        }
    }
}
