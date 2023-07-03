using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Responses.Users;

namespace Anubis.Application.Requests.Users
{
    public record GetMyPatientsRequest
    {
        public string? MedicId { get; set; }
    }
}
