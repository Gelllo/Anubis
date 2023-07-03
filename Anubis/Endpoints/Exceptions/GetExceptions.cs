using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using Anubis.Application.Requests.ApplicationExceptions;
using Anubis.Application.Responses.ApplicationExceptions;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Exceptions
{
    public class GetExceptions : EndpointWithoutRequest
    {
        private readonly IQueryDispatcher _dispatcher;
        private ILogger _logger;

        public GetExceptions(IQueryDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }
        public override void Configure()
        {
            Get("/exceptions");
            Roles("ADMIN");
        }

        public override async Task HandleAsync(CancellationToken c)
        {
            var req = new GetExceptionsRequest()
            {
                order = Query<string>("order"),
                sort = Query<string>("sort"),
                page = Query<int>("page")
            };

            await SendAsync(await _dispatcher.Dispatch<GetExceptionsRequest, GetExceptionsResponse>(req, c));
        }
    }
}
