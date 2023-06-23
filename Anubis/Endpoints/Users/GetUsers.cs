using FastEndpoints;
using Anubis.Application;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Anubis.Web.Endpoints.Users
{

    public class GetUsers : Endpoint<GetUsersRequest, GetUsersResponse>
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

        public override async Task HandleAsync(GetUsersRequest r, CancellationToken c)
        { 
            await SendAsync(await _dispatcher.Dispatch<GetUsersRequest, GetUsersResponse>(r, c));
        }
    }
}
