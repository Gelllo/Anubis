using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain;
using Anubis.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Anubis.Infrastracture.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository, IDisposable
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetAllAsync();
        }

        public async Task<User> GetUserByID(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            return await _dbContext.Users.Where(x => x.UserID == userId).FirstOrDefaultAsync();
        }

        public async Task<int> InsertUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            Save();
            return user.Id;
        }

        public void DeleteUser(int userID)
        {
            User user = _dbContext.Users.Find(userID);
            _dbContext.Users.Remove(user);
            Save();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            Save();
            return user;
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
