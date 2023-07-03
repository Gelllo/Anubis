using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;

namespace Anubis.Application.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        Task<IEnumerable<User>> GetUsersAsync(string? sort, string? order, int? page);
        Task<IEnumerable<User>> GetPatientsForMedic(string medicID);
        Task<int> AddPatient(string medicId,string patientEmail);
        Task DeletePatient(string medicId, string patientId);
        Task<User> GetUserByID(int userId);
        Task<User> GetUserByUserId(string? userId);
        Task<User> GetUserByToken(string token);
        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByResetToken(string resetToken);
        Task<int> InsertUserAsync(User user);
        void DeleteUser(int userId);
        Task<User> UpdateUserAsync(User user);
        void Save();
    }
}
