using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Anubis.Domain.UsersDomain
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

        [Column("PasswordSalt", TypeName = "nvarchar(MAX)")]
        public byte[]? PasswordSalt { get; set; }

        [Column("PasswordHash", TypeName = "nvarchar(MAX)")]
        public byte[]? PasswordHash { get; set; }

        [Column("Role", TypeName = "nvarchar(200)"), Required]
        public string Role { get; set; }

        [Column("PasswordResetToken", TypeName = "nvarchar(200)")]
        public string? PasswordResetToken { get; set; }
        
        [Column("ResetTokenExpires", TypeName = "DateTime")]
        public DateTime? ResetTokenExpires { get; set; }

        public virtual UserRefreshToken? RefreshToken { get; set; }

        public virtual ICollection<User>? Patients { get; set; }

        public virtual ICollection<User>? Medics { get; set; }
    }

    public static class UserRoles
    {
        public static readonly string[] roles = { "ADMIN", "USER", "MEDIC" };

        public static readonly string ADMIN = "ADMIN";

        public static readonly string USER = "USER";

        public static readonly string MEDIC = "MEDIC";
    }
}