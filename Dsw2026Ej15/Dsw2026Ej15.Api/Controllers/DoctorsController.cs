using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("Nombre y matrícula requeridos");
            }

            var speciality = await _persistence.GetSpecialityById(request.SpecialityId);

            if (speciality == null)
            {
                var specialitiyId = request.SpecialityId == Guid.Empty ? Guid.NewGuid() : request.SpecialityId;

                speciality = new Speciality("Especialidad General", "Creada automáticamente", specialitiyId);
            }

            var newDoctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            var doctor = await _persistence.AddDoctor(newDoctor);

            return Created("", $"Se creó el médico: {doctor.Name}, Id: {doctor.Id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            var doctors = await _persistence.GetDoctors();
            return Ok(doctors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _persistence.GetDoctorById(id);
            if (doctor == null || !doctor.IsActive)
            {
                return NotFound();
            }

            doctor.IsActive = false;
            await _persistence.UpdateDoctorAsync(doctor); // ¡Crucial para impactar en la BD!

            return NoContent();
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorActive(Guid doctorId)
        {
            var doctor = await _persistence.GetDoctorById(doctorId);
            if (doctor == null || !doctor.IsActive)
            {
                return NotFound("El médico no existe o no está activo.");
            }

            return Ok($"DATOS DEL MÉDICO: - Name: {doctor.Name} | - License Number: {doctor.LicenseNumber} | - Speciality Name: {doctor.Speciality?.Name}");
        }
    }
}
