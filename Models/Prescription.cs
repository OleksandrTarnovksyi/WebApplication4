using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    [Table("Prescription")]
    [PrimaryKey("IdPrescription")]
    public class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }

        [Column(TypeName = "datetime")]
        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName = "datetime")]
        [Required]
        public DateTime DueDate { get; set; }

        [Key]
        public int IdDoctor { get; set; }

        [Key]
        public int IdPatient { get; set; }

        public Patient Patient { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;

        public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = new List<Prescription_Medicament>();
    }
}
