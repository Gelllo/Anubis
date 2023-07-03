using Anubis.Application.Requests.Users;
using Anubis.Application;
using Anubis.Application.Responses.Users;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Users
{
    public class AddPatient : Endpoint<AddPatientRequest, AddPatientResponse>
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public AddPatient(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Put("/users/mypatients/{MedicID}");
            Roles("MEDIC");
        }

        public override async Task HandleAsync(AddPatientRequest req, CancellationToken ct)
        {
            req.MedicUsername = Route<string>("MedicID");
            await SendAsync(await _dispatcher.Dispatch<AddPatientRequest, AddPatientResponse>(req, ct));
        }
    }
}