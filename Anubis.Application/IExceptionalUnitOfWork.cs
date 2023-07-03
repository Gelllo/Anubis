using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Anubis.Application
{
    public interface IExceptionalUnitOfWork<TContext> : IBaseUnitOfWork<TContext> where TContext : DbContext
    {
        IApplicationExceptionsRepository ApplicationExceptionsRepository { get; }
    }
}
