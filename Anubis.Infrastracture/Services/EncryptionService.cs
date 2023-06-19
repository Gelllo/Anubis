using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Responses.Register;
using Anubis.Domain;

namespace Anubis.Infrastracture.Services
{
    public static class EncryptionService
    {
        public static User EncryptPasswordForUser(this User user, string password)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return user;
        }

        public static bool CheckPasswordForUser(User user, string password)
        {
            bool result = false;

            using (HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }

            return result;
        }
    }
}
