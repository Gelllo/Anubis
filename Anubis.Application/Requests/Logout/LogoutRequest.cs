using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.Logout
{
    public record LogoutRequest
    {
        public string UserID { get; set; }
    }
}
