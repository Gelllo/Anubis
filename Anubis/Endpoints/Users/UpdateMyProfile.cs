using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Users
{
    public class UpdateMyProfile : Endpoint<UpdateMyProfileRequest, UpdateMyProfileResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public UpdateMyProfile(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Put("/users/{UserID}");
        }

        public override async Task HandleAsync(UpdateMyProfileRequest req, CancellationToken ct)
        {
            try
            {
                req.UserID = Route<string>("UserID");
                await SendAsync(await _dispatcher.Dispatch<UpdateMyProfileRequest, UpdateMyProfileResponse>(req, ct));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
