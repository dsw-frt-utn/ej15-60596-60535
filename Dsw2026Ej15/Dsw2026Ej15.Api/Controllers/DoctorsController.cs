
using Dsw2026Ej15.Api.Models;
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

            return Created(); //Devuelve el código de estado 201
        }
    }
}
