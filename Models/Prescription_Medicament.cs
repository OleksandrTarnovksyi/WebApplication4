using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    [Table("Prescription_Medicament")]
    [PrimaryKey("IdMedicament", "IdPrescription")]
    public class Prescription_Medicament
    {
        [Key]
        public int IdMedicament { get; set; }

        [Key]
        public int IdPrescription { get; set; }

        public int? Dose { get; set; }

        [MaxLength(100)]
        [Required]
        public string Details { get; set; } = null!;

        public Medicament Medicament { get; set; } = null!;

        public Prescription Prescription { get; set; } = null!;
    }
}
