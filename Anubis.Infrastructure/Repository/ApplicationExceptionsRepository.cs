using Anubis.Application.Repository;
using Anubis.Domain.UsersDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.ExceptionsDomain;
using Microsoft.EntityFrameworkCore;

namespace Anubis.Infrastructure.Repository
{
    public class ApplicationExceptionsRepository : IApplicationExceptionsRepository, IDisposable
    {
        private ExceptionalContext _dbContext;
        public ApplicationExceptionsRepository(ExceptionalContext exceptionalContext)
        {
            this._dbContext = exceptionalContext;
        }

        public async Task<IEnumerable<MyApplicationException>> GetApplicationExceptions(string? sort, string? order,int? page)
        {
            var queryable = _dbContext.MyApplicationExceptions.AsQueryable();

            if (order == "asc")
            {
                switch (sort)
                {
                    case "id":
                        queryable = queryable.OrderBy(x => x.Id);
                        break;
                    case "application":
                        queryable = queryable.OrderBy(x => x.Application);
                        break;
                    case "dateThrown":
                        queryable = queryable.OrderBy(x => x.DateThrown);
                        break;
                    case "error":
                        queryable = queryable.OrderBy(x => x.Error);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sort)
                {
                    case "id":
                        queryable = queryable.OrderByDescending(x => x.Id);
                        break;
                    case "application":
                        queryable = queryable.OrderByDescending(x => x.Application);
                        break;
                    case "dateThrown":
                        queryable = queryable.OrderByDescending(x => x.DateThrown);
                        break;
                    case "error":
                        queryable = queryable.OrderByDescending(x => x.Error);
                        break;
                    default:
                        break;
                }
            }

            return await queryable.Skip((page ?? 0) * 30).Take(30).ToListAsync();
        }

        public async Task<int> GetExceptionsCount()
        {
            return await Task.FromResult(_dbContext.MyApplicationExceptions.Count());
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
