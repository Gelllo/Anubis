using Anubis.Application.Repository;
using Anubis.Application;
using Anubis.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Infrastructure
{
    public class ExceptionalUnitOfWork : IExceptionalUnitOfWork<ExceptionalContext>, IDisposable
    {

        private readonly ExceptionalContext _dbContext;

        public ExceptionalUnitOfWork(ExceptionalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IApplicationExceptionsRepository ApplicationExceptionsRepository { get; }


        public void Commit()
            => _dbContext.SaveChanges();


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _dbContext.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
