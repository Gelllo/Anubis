using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.ExternalLogin
{
    public record GoogleLoginRequest
    {
        public string Credentials { get; set; }
    }
}
