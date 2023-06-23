using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;

namespace Anubis.Application.Responses.Users
{
    public record CreateUserResponse
    {
        public User? User { get; set; }
    }
}
