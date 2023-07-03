using FastEndpoints;
using Anubis.Application;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Anubis.Application.Requests.ApplicationExceptions;

namespace Anubis.Web.Endpoints.Users
{

    public class GetUsers : EndpointWithoutRequest
    {
        private readonly IQueryDispatcher _dispatcher;
        private ILogger _logger;

        public GetUsers(IQueryDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }
        public override void Configure()
        {
            Get("/users/");
            Roles("ADMIN");
        }

        public override async Task HandleAsync(CancellationToken c)
        {
            var req = new GetUsersRequest()
            {
                order = Query<string>("order"),
                sort = Query<string>("sort"),
                page = Query<int>("page")
            };

            await SendAsync(await _dispatcher.Dispatch<GetUsersRequest, GetUsersResponse>(req, c));
        }
    }
}
