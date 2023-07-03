using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Responses.Login
{
    public record LoginResponse
    {
        public string? UserID { get; set; }

        public string Role { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }
    }
}
