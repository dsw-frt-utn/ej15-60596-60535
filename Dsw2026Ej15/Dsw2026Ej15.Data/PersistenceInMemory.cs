using Dsw2026Ej15.Domain;
using System.Text.Json;
namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory
    {
        private List <Doctor> doctors;
        private List<Speciality> specialities;

        public void AgregarDoctor(string name, string licenseNum, Speciality speciality)
        {
            Doctor doctor = new Doctor(name, licenseNum, speciality);
            doctors.Add(doctor);
        }

        public List<Doctor> ListarDoctores()
        {
            return doctors;
        }

        public void AgregarEspecialidad(string name, string descripcion)
        {
            Speciality speciality = new Speciality(name, descripcion);
            specialities.Add(speciality);
        }

        public List<Speciality> ListarEspecialidades()
        {
            return specialities;
        }
        private async void LoadSpecialities()
        {
            var json = await File.ReadAllTextAsync("specialties.json");
            var products = JsonSerializer.Deserialize<List<Speciality>>(json);
        }
    }
}
