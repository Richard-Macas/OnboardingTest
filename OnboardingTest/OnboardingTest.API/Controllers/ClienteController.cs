using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;

namespace OnboardingTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService servicio;

        public ClienteController(IClienteService _servicio)
        {
            servicio = _servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            return Ok(await servicio.GetClientes());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCliente(int Id)
        {
            var cliente = await servicio.GetCliente(Id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> PostCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sameClienteIdentification = await servicio.GetCliente(x => x.Identificacion == cliente.Identificacion);

                    if (sameClienteIdentification == null)
                        return Ok(await servicio.InsertCliente(cliente));
                    else
                        return Conflict("Ya existe un cliente registrado con el mismo número de identificación");
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
        public async Task<IActionResult> PutCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sameClienteIdentification = await servicio.GetCliente(x => x.Identificacion == cliente.Identificacion && x.Id != cliente.Id);

                    var clienteExiste = await servicio.GetCliente(cliente.Id);

                    // Se valida que el cliente exista
                    if (clienteExiste != null)
                    {
                        if (sameClienteIdentification == null)
                            return Ok(await servicio.UpdateCliente(cliente));
                        else
                            return Conflict("Ya existe un cliente registrado con el mismo número de identificación");
                    }
                    else
                        return NotFound($"No se encontró el cliente con Id: {cliente.Id}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("El modelo de tipo Cliente está incorrecto");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCliente(int Id)
        {
            try
            {
                var clienteExiste = await servicio.GetCliente(Id);

                if (clienteExiste != null)
                    return Ok(await servicio.DeleteCliente(Id));
                else
                    return NotFound($"No se encontró el cliente con Id: {Id}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


    }
}
