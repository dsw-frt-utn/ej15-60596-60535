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

        private void LoadSpecialities()
        {
            string fileName = "especialidades.json";
            var json = await File.ReadAllTextAsync("products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json);
        }
    }
}
