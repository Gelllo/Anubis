using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain;

namespace Anubis.Application.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByID(int userId);
        Task<User> GetUserByUserId(string userId);
        Task<int> InsertUserAsync(User user);
        void DeleteUser(int userId);
        Task<User> UpdateUserAsync(User user);
        void Save();
    }
}
