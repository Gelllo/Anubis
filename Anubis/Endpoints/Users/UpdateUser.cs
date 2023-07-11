using FastEndpoints;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using Google.Apis.Requests;

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
            Roles("ADMIN");
        }

        public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        {
            try
            {
                await SendAsync(await _dispatcher.Dispatch<UpdateUserRequest, UpdateUserResponse>(req, ct));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
