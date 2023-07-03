using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.ExceptionsDomain;

namespace Anubis.Application.Repository
{
    public interface IApplicationExceptionsRepository
    {
        Task<IEnumerable<MyApplicationException>> GetApplicationExceptions(string? sort, string? order, int? page);

        Task<int> GetExceptionsCount();
    }
}
