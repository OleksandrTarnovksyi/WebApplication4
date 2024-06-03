using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication4.Models;


namespace WebApplication4.Context
{
    public class MedDBContext : DbContext
    {


        public MedDBContext(DbContextOptions<MedDBContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public  DbSet<Medicament> Medicaments { get; set; }
        public  DbSet<Patient> Patients { get; set; }
        public  DbSet<Prescription> Prescriptions { get; set; }
        public  DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription_Medicament>()
                      .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

            modelBuilder.Entity<Prescription>().HasKey(p => p.IdPrescription);

            modelBuilder.Entity<Medicament>().HasKey(m => m.IdMedicament);

            modelBuilder.Entity<Doctor>().HasKey(d => d.IdDoctor);

            modelBuilder.Entity<Patient>().HasKey(p => p.IdPatient);

            modelBuilder.Entity<Prescription>()
                     .HasMany(p => p.Prescription_Medicaments)
                     .WithOne(p => p.Prescription)
                     .HasForeignKey(p => p.IdPrescription);

            modelBuilder.Entity<Medicament>().HasMany(m => m.Prescription_Medicaments)
                    .WithOne(m => m.Medicament)
                    .HasForeignKey(m => m.IdMedicament);

            modelBuilder.Entity<Doctor>()
                    .HasMany(d => d.Prescriptions)
                    .WithOne(d => d.Doctor)
                    .HasForeignKey(d => d.IdDoctor);

            modelBuilder.Entity<Patient>()
                    .HasMany(p => p.Prescriptions)
                    .WithOne(p => p.Patient)
                    .HasForeignKey(p => p.IdPatient);
        }

    }
}
