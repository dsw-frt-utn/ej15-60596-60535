
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        //Todos los métodos del controlador convienen que sean asíncronos
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request) //El [FromBody] indica que se deben recuperar los datos del bpdy
        {
            //return Ok("Hola Mundo"); Devuelve un mensaje con el código de estado 200

            //Validaciones
            if(string.IsNullOrWhiteSpace(request.Name) || 
                string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y Matrícula son requeridos");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if(speciality == null)
            {
                return BadRequest("La especialidad no existe");
            }

            _persistence.AddDoctor(request.Name, request.LicenseNumber, speciality);
            return Created(); //Devuelve el código de estado 201
            
        }

        [HttpGet]

        public IActionResult GetActiveDoctors()
        {
            var doctors = _persistence.GetDoctors();

            return Ok(doctors);
        }

        [HttpDelete("{id}")]


        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);
            if(doctor == null || doctor.IsActive == false)
            {
                return NotFound();
            }
            
            doctor.IsActive = false;
            return NoContent();

        }
    }
}
