using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Responses.Login
{
    public record LoginResponse
    {
        public string TokenJWT { get; set; }

        public string UserID { get; set; }
    }
}
