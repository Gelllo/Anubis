using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Domain.UsersDomain
{
    [Table("UserRefreshTokens")]
    public class UserRefreshToken
    {
        [Column("Token", TypeName = "nvarchar(200)"), Required]
        public string Token { get; set; }

        [Column("Created", TypeName = "DateTime"), Required]
        public DateTime Created { get; set; }

        [Column("Expires", TypeName = "DateTime"), Required]
        public DateTime Expires { get; set; }

        [Key, Column("UserID", TypeName = "nvarchar(200)")]
        public string UserID { get; set; }

        public virtual User User { get; set; }
    }
}
