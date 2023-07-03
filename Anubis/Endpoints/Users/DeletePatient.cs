using Anubis.Application.Requests.Users;
using Anubis.Application;
using Anubis.Application.Responses.Users;
using FastEndpoints;

namespace Anubis.Web.Endpoints.Users
{
    public class DeletePatient : EndpointWithoutRequest
    {
        private ICommandDispatcher _dispatcher;
        private ILogger _logger;

        public DeletePatient(ICommandDispatcher dispatcher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public override void Configure()
        {
            Delete("/users/mypatients/{MedicID}/{PatientID}");
            Roles("MEDIC");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var req = new DeletePatientRequest()
            {
                MedicID = Route<string>("MedicID"),
                PatientID = Route<string>("PatientID")
            };

            await SendAsync(await _dispatcher.Dispatch<DeletePatientRequest, DeletePatientResponse>(req, ct));
        }
    }
}