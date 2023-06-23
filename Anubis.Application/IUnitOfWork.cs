using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Repository;

namespace Anubis.Application
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IUserRefreshTokenRepository UserRefreshTokenRepository { get; }

        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
