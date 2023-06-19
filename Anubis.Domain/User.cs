using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anubis.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column("UserID", TypeName = "nvarchar(200)"), Required]
        public string UserID { get; set; }

        [Column("LastName", TypeName = "nvarchar(200)"), Required]
        public string LastName { get; set; }

        [Column("FirstName", TypeName = "nvarchar(200)"), Required]
        public string FirstName { get; set; }

        [Column("Email", TypeName = "nvarchar(200)"), Required]
        public string Email { get; set; }

        [Column("PasswordSalt", TypeName = "nvarchar(MAX)"), Required]
        public byte[] PasswordSalt { get; set; }

        [Column("PasswordHash", TypeName = "nvarchar(MAX)"), Required]
        public byte[] PasswordHash { get; set; }

        [Column("Role", TypeName = "nvarchar(200)"), Required]
        public string Role { get; set; }
    }

    public static class UserRoles
    {
        public static readonly string[] roles = { "Admin" , "User" , "Medic" };
    }
}