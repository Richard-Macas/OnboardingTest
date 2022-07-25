using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;

namespace OnboardingTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientePatioController : ControllerBase
    {
        private readonly IClientePatioService servicio;

        public ClientePatioController(IClientePatioService _servicio)
        {
            servicio = _servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientePatios()
        {
            return Ok(await servicio.GetClientePatios());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClientePatio(int Id)
        {
            var clientePatio = await servicio.GetClientePatio(Id);
            if (clientePatio == null)
            {
                return NotFound();
            }

            return Ok(clientePatio);
        }

        [HttpPost]
        public async Task<IActionResult> PostClientePatio(ClientePatio clientePatio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(await servicio.InsertClientePatio(clientePatio));
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
        public async Task<IActionResult> PutClientePatio(ClientePatio clientePatio)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var clientePatioExiste = await servicio.GetClientePatio(clientePatio.Id);

                    if (clientePatioExiste != null)
                        return Ok(await servicio.UpdateClientePatio(clientePatio));

                    else
                        return NotFound($"No se encontró el clientePatio con Id: {clientePatio.Id}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("El modelo de tipo ClientePatio está incorrecto");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClientePatio(int Id)
        {
            try
            {
                var clientePatioExiste = await servicio.GetClientePatio(Id);

                if (clientePatioExiste != null)
                    return Ok(await servicio.DeleteClientePatio(Id));
                else
                    return NotFound($"No se encontró el clientePatio con Id: {Id}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


    }
}
