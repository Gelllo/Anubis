using Anubis.Domain.UsersDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Repository
{
    public interface IUserRefreshTokenRepository
    {
        IEnumerable<UserRefreshToken> GetRefreshTokens();

        Task<IEnumerable<UserRefreshToken>> GetRefreshTokensAsync();
        Task<UserRefreshToken> GetRefreshTokenByUserID(string userId);
        void InsertRefreshTokenAsync(UserRefreshToken token);
        void DeleteRefreshTokenByUserId(string userId);
        Task<UserRefreshToken> UpdateRefreshTokenAsync(UserRefreshToken token);
        void Save();
    }
}
