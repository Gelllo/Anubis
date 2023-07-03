using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Anubis.Domain.UsersDomain;

namespace Anubis.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository, IDisposable
    {
        public UserRepository(AnubisContext context) : base(context)
        {

        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string? sort, string? order, int? page)
        {
            var queryable = _dbContext.Users.AsQueryable();

            if (order == "asc")
            {
                switch (sort)
                {
                    case "Id":
                        queryable = queryable.OrderBy(x => x.Id);
                        break;
                    case "UserID":
                        queryable = queryable.OrderBy(x => x.UserID);
                        break;
                    case "LastName":
                        queryable = queryable.OrderBy(x => x.LastName);
                        break;
                    case "FirstName":
                        queryable = queryable.OrderBy(x => x.FirstName);
                        break;
                    case "Email":
                        queryable = queryable.OrderBy(x => x.Email);
                        break;
                    case "Role":
                        queryable = queryable.OrderBy(x => x.Role);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sort)
                {
                    case "Id":
                        queryable = queryable.OrderByDescending(x => x.Id);
                        break;
                    case "UserID":
                        queryable = queryable.OrderByDescending(x => x.UserID);
                        break;
                    case "LastName":
                        queryable = queryable.OrderByDescending(x => x.LastName);
                        break;
                    case "FirstName":
                        queryable = queryable.OrderByDescending(x => x.FirstName);
                        break;
                    case "Email":
                        queryable = queryable.OrderByDescending(x => x.Email);
                        break;
                    case "Role":
                        queryable = queryable.OrderByDescending(x => x.Role);
                        break;
                    default:
                        break;
                }
            }

            return await queryable.Skip((page ?? 0) * 30).Take(30).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetPatientsForMedic(string medicID)
        {
            var medic = await GetUserByUserId(medicID);

            var users = await _dbContext.Users.AsQueryable().Where(x => x.Medics.Contains(medic)).ToListAsync();

            return users;
        }

        public async Task<int> AddPatient(string medicID, string patientEmail)
        {
            var medic = await GetUserByUserId(medicID);
            var patient = await GetUserByEmail(patientEmail);

            medic.Patients.Add(patient);

            _dbContext.Entry(medic).State = EntityState.Modified;
            Save();

            return medic.Id;
        }

        public async Task DeletePatient(string medicId, string patientId)
        {
            var medic = await GetUserByUserId(medicId);
            var patient = await GetUserByUserId(patientId);

            if (patient != null && medic.Patients.Any())
            {
                medic.Patients.Remove(patient);
            }

            Save();
        }

        public async Task<User> GetUserByID(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUserId(string? userId)
        {
            var user = await _dbContext.Users.Where(x => x.UserID == userId).Include(y=>y.RefreshToken).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByToken(string token)
        {
            return await _dbContext.Users.Where(x => x.RefreshToken.Token == token).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByResetToken(string resetToken)
        {
            return await _dbContext.Users.AsQueryable().Where(x=>x.PasswordResetToken == resetToken).FirstOrDefaultAsync();
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
            if (user != null)
            {
                if (user.Medics.Any())
                {
                    foreach (var medic in user.Medics)
                    {
                        medic.Patients.Remove(user);
                    }
                }
            }
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
