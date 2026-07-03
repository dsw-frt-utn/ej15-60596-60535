using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEf(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _context.Doctors
                                 .Include(d => d.Speciality)
                                 .Where(d => d.IsActive)
                                 .ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityById(Guid? id)
        {
            if (id == null) return null;
            return await _context.Specialities.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors
                                 .Include(d => d.Speciality)
                                 .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Speciality>> ListarEspecialidades()
        {
            return await _context.Specialities.ToListAsync();
        }
    }
}
