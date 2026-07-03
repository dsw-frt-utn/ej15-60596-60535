using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Doctor> _doctors = [];
        private List<Speciality> _specialities = [];

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public Task<Doctor> AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
            return Task.FromResult(doctor);
        }

        public Task<IEnumerable<Doctor>> GetDoctors()
        {
            var activeDoctors = _doctors.Where(d => d.IsActive);
            return Task.FromResult<IEnumerable<Doctor>>(activeDoctors);
        }

        public Task<List<Speciality>> ListarEspecialidades()
        {
            return Task.FromResult(_specialities);
        }

        public Task<Speciality?> GetSpecialityById(Guid? id)
        {
            var speciality = _specialities.SingleOrDefault(e => e.Id == id);
            return Task.FromResult(speciality);
        }

        public Task<Doctor?> GetDoctorById(Guid id)
        {
            var doctor = _doctors.SingleOrDefault(e => e.Id == id);
            return Task.FromResult(doctor);
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            var existing = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existing != null)
            {
                existing.IsActive = doctor.IsActive;
            }
            return Task.CompletedTask;
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                if (!File.Exists(jsonPath)) return;

                var json = File.ReadAllText(jsonPath);

                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? [];

                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class SpecialityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

