
using Anubis.Application.Repository;
using Microsoft.EntityFrameworkCore;
using Anubis.Domain.UsersDomain;

namespace Anubis.Infrastructure.Repository
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository, IDisposable
    {
        public UserRefreshTokenRepository(AnubisContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UserRefreshToken> GetRefreshTokens()
        {
            return _dbContext.RefreshTokens.ToList();
        }

        public async Task<IEnumerable<UserRefreshToken>> GetRefreshTokensAsync()
        {
            return await GetAllAsync();
        }

        public async Task<UserRefreshToken> GetRefreshTokenByUserID(string userId)
        {
            return await _dbContext.RefreshTokens.Where(x => x.UserID == userId).FirstOrDefaultAsync();
        }

        public async void InsertRefreshTokenAsync(UserRefreshToken token)
        {
            await _dbContext.RefreshTokens.AddAsync(token);
            Save();
        }

        public void DeleteRefreshTokenByUserId(string userId)
        {
            UserRefreshToken token =  _dbContext.RefreshTokens.FirstOrDefault(x => x.UserID == userId);
            _dbContext.RefreshTokens.Remove(token);
            Save();
        }

        public async Task<UserRefreshToken> UpdateRefreshTokenAsync(UserRefreshToken token)
        {
            _dbContext.Entry(token).State = EntityState.Modified;
            Save();
            return token;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
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
