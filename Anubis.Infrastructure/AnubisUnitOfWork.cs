﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application;
using Anubis.Application.Repository;
using Anubis.Infrastructure.Repository;

namespace Anubis.Infrastructure
{
    public class AnubisUnitOfWork : IAnubisUnitOfWork<AnubisContext>, IDisposable
    {
        private readonly AnubisContext _dbContext;
        private IUserRepository _userRepository;
        private IUserRefreshTokenRepository _userRefreshTokenRepository;

        public AnubisUnitOfWork(AnubisContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_dbContext); }
        }

        public IUserRefreshTokenRepository UserRefreshTokenRepository
        {
            get { return _userRefreshTokenRepository = _userRefreshTokenRepository ?? new UserRefreshTokenRepository(_dbContext); }
        }

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
