using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.Users
{
    public record AddPatientRequest
    {
        public string PatientEmail { get; set; }
        public string? MedicUsername { get; set; }
    }
}
