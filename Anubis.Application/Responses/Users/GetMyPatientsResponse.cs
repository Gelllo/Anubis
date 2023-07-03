using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Responses.Users
{
    public record GetMyPatientsResponse
    {
        public IEnumerable<UserDTO>? Patients { get; set; }
    }
}
