using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.ApplicationExceptions
{
    public record GetExceptionsRequest
    {
        public string? sort { get; set; }

        public string? order { get; set; }

        public int? page { get; set; }
    }
}
