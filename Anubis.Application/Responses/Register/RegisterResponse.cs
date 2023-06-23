using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;

namespace Anubis.Application.Responses.Register
{
    public record RegisterResponse
    {
        public User? user { get; set; }
    }
}
