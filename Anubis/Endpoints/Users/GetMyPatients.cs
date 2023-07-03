using FastEndpoints;
using Anubis.Application;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Anubis.Application.Requests.ApplicationExceptions;

namespace Anubis.Web.Endpoints.Users
{

    public class GetMyPatients : EndpointWithoutRequest
    {
        private readonly IQueryDispatcher _dispatcher;
        private ILogger _logger;

        public GetMyPatients(IQueryDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }
        public override void Configure()
        {
            Get("/users/mypatients/{MedicID}");
            Roles("MEDIC");
        }

        public override async Task HandleAsync(CancellationToken c)
        {
            var req = new GetMyPatientsRequest()
            {
                MedicId = Route<string>("MedicID")
            };
            await SendAsync(await _dispatcher.Dispatch<GetMyPatientsRequest, GetMyPatientsResponse>(req, c));
        }
    }
}