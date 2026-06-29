using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List <Doctor> _doctors = [];
        private List<Speciality> _specialities = [];

        public Doctor AddDoctor(string name, string license, Speciality speciality)
        {
            Doctor doctor = new Doctor(name, license, speciality);
            _doctors.Add(doctor);
            return doctor;
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        //public void AgregarEspecialidad(string name, string descripcion, Guid id)
        //{
        //    Speciality speciality = new Speciality (name, descripcion, id);
        //    _specialities.Add(speciality);
        //}

        public List<Speciality> ListarEspecialidades()
        {
            return _specialities;
        }

        public Speciality? GetSpecialityById(Guid? id)
        {
            return _specialities.SingleOrDefault(e => e.Id == id);
        }

        public Doctor? GetDoctorById(Guid? id)
        {
            return _doctors.SingleOrDefault(e => e.Id == id);
        }

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }
        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true}) ?? [];
                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

    }
}
