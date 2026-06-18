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

        public List<Doctor> GetDoctors();

        public void AgregarDoctor(string name, string licenseNum, Speciality speciality);

        public Speciality? GetSpecialityById(Guid? id);


    }
}
