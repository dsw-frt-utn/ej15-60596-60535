using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        public void AgregarEspecialidad(string nombre, string descripcion);
        public List<Speciality> ListarEspecialidades();

        public List<Doctor> ListarDoctores();

        public void AgregarDoctor(string name, string licenseNum, Speciality speciality);


    }
}
