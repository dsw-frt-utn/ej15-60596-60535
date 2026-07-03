using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        // public void AgregarEspecialidad(string nombre, string descripcion);
        Task<List<Speciality>> ListarEspecialidades();
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Speciality?> GetSpecialityById(Guid? id);
        Task<Doctor?> GetDoctorById(Guid id);
        Task<Doctor> AddDoctor(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
    }
}
