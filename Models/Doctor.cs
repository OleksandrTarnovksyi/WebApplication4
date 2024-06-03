using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace WebApplication4.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; } = null!;

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; } = null!;

        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = null!;

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
