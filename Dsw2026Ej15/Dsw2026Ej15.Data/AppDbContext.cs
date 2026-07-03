using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Speacilities { get; set; }

        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
