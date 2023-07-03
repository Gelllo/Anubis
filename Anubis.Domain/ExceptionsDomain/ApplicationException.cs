using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Domain.ExceptionsDomain
{
    public record MyApplicationException
    {
        [Key]
        public int Id { get; set; }

        [Column("MessageTemplate", TypeName = "nvarchar(200)")]
        public string ?MessageTemplate { get; set; }

        [Column("DateThrown", TypeName = "datetime"), Required]
        public DateTime DateThrown { get; set; }

        [Column("Error", TypeName = "nvarchar(200)")]
        public string? Error { get; set; }

        [Column("Application", TypeName = "nvarchar(200)")]
        public string? Application { get; set; }
    }
}
