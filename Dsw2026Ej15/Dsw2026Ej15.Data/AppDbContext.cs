using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Insertamos una especialidad por defecto para pruebas
            modelBuilder.Entity<Speciality>().HasData(
                new Speciality(
                    "Pediatría",
                    "Especialidad médica infantil",
                    Guid.Parse("8a1f3b78-3f66-4d68-8d6e-1c5b9c7a2f41") // El mismo ID de tu Swagger
                )
            );
        }
    }
}
