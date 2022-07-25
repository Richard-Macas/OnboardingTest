using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;

namespace OnboardingTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioService servicio;

        public PatioController(IPatioService _servicio)
        {
            servicio = _servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatios()
        {
            return Ok(await servicio.GetPatios());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPatio(int Id)
        {
            var patio = await servicio.GetPatio(Id);
            if (patio == null)
            {
                return NotFound();
            }

            return Ok(patio);
        }

        [HttpPost]
        public async Task<IActionResult> PostPatio(Patio patio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(await servicio.InsertPatio(patio));
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
        public async Task<IActionResult> PutPatio(Patio patio)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var patioExiste = await servicio.GetPatio(patio.Id);

                    if (patioExiste != null)
                        return Ok(await servicio.UpdatePatio(patio));

                    else
                        return NotFound($"No se encontró el patio con Id: {patio.Id}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("El modelo de tipo Patio está incorrecto");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePatio(int Id)
        {
            try
            {
                var patioExiste = await servicio.GetPatio(Id);

                if (patioExiste != null)
                    return Ok(await servicio.DeletePatio(Id));
                else
                    return NotFound($"No se encontró el patio con Id: {Id}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


    }
}
