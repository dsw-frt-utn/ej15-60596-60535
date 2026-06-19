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
        public List<Speciality> ListarEspecialidades();

        public List<Doctor> ListarDoctores();

        public Speciality? GetSpecialityById(Guid? id);

        public Doctor? GetDoctorById(Guid? id);
        public Doctor AddDoctor(string name, string license, Speciality speciality);
    }
}
