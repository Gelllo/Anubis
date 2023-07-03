using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Anubis.Application
{
    public interface IBaseUnitOfWork<TContext> where TContext : DbContext
    {
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
