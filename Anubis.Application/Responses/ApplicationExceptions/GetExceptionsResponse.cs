using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.ExceptionsDomain;

namespace Anubis.Application.Responses.ApplicationExceptions
{
    public record GetExceptionsResponse
    {
        public IEnumerable<MyApplicationException> ApplicationExceptions { get; set; }

        public int total_count { get; set; }
    }
}
