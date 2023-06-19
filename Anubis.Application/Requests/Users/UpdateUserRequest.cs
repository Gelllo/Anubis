using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.Users
{
    public record UpdateUserRequest
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
