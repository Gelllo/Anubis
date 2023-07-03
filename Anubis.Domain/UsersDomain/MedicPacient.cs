using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Domain.UsersDomain
{
    [Table("MedicPatient")]
    public class MedicPatient
    {
        [Key]
        public int Id { get; set; }

        [Column("Medic", TypeName = "int")]
        public int? MedicID { get; set; }

        public virtual User Medic { get; set; } = new User();

        [Column("Patient", TypeName = "int")]
        public int? PatientID { get; set; }

        public virtual User Patient { get; set; } = new User();
    }
}
