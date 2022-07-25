using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;

namespace OnboardingTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService servicio;

        public VehiculoController(IVehiculoService _servicio)
        {
            servicio = _servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiculos(string Filter)
        {
            return Ok(await servicio.GetVehiculos(x => x.IdMarcaNavigation.Nombre.ToLower() == Filter.ToLower() 
                                                    || x.Modelo.ToLower() == Filter.ToLower() 
                                                    || x.Anio.ToString() == Filter));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetVehiculo(int Id)
        {
            var vehiculo = await servicio.GetVehiculo(Id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> PostVehiculo(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sameVehiculoIdentification = await servicio.GetVehiculo(x => x.Placa == vehiculo.Placa);

                    if (sameVehiculoIdentification == null)
                        return Ok(await servicio.InsertVehiculo(vehiculo));
                    else
                        return Conflict("Ya existe un vehiculo registrado con el mismo número de placa");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("El modelo está incorrecto");
        }

        [HttpPut]
        public async Task<IActionResult> PutVehiculo(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sameVehiculoIdentification = await servicio.GetVehiculo(x => x.Placa == vehiculo.Placa && x.Id != vehiculo.Id);

                    var vehiculoExiste = await servicio.GetVehiculo(vehiculo.Id);

                    // Se valida que el vehiculo exista
                    if (vehiculoExiste != null)
                    {
                        if (sameVehiculoIdentification == null)
                            return Ok(await servicio.UpdateVehiculo(vehiculo));
                        else
                            return Conflict("Ya existe un vehiculo registrado con el mismo número de placa");
                    }
                    else
                        return NotFound($"No se encontró el vehiculo con Id: {vehiculo.Id}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("El modelo de tipo Vehiculo está incorrecto");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteVehiculo(int Id)
        {
            try
            {
                var vehiculoExiste = await servicio.GetVehiculo(Id);

                if (vehiculoExiste != null)
                    return Ok(await servicio.DeleteVehiculo(Id));
                else
                    return NotFound($"No se encontró el vehiculo con Id: {Id}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


    }
}
