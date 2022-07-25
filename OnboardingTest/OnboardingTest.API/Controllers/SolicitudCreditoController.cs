using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;

namespace OnboardingTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCreditoController : ControllerBase
    {
        private readonly ISolicitudCreditoService servicio;
        private readonly ITrackingSolicitudService servicioTra;
        private readonly IClientePatioService servicioCP;

        public SolicitudCreditoController(ISolicitudCreditoService _servicio, ITrackingSolicitudService _servicioTra, IClientePatioService _servicioCP)
        {
            servicio = _servicio;
            servicioTra = _servicioTra;
            servicioCP = _servicioCP;
        }

        [HttpGet]
        public async Task<IActionResult> GetSolicitudCreditos()
        {
            return Ok(await servicio.GetSolicitudCreditos());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSolicitudCredito(int Id)
        {
            var solicitudCredito = await servicio.GetSolicitudCredito(Id);
            if (solicitudCredito == null)
            {
                return NotFound();
            }

            return Ok(solicitudCredito);
        }

        [HttpPost]
        public async Task<IActionResult> PostSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fechaActual = DateTime.Now.Date.ToString().Split(' ')[0];
                    var fechaActualDateOnly = DateOnly.Parse(fechaActual);
                    // Seteamos la fecha actual a la solicitud
                    solicitudCredito.FechaElaboracion = fechaActualDateOnly;

                    // Validar si hay solicitudes con el vechículo seleccionado que están en proceso
                    var solicitudMismoVehiculo = await servicio.GetSolicitudCredito(x => x.IdVehiculo == solicitudCredito.IdVehiculo
                                                                                && x.TrackingSolicituds.Where(t => t.Estado.Equals("REGISTRADA")).Count() > 0 
                                                                                && x.TrackingSolicituds.Count() == 1
                                                                                );

                    if (solicitudMismoVehiculo != null)
                        return Conflict("El vehículo seleccionado se encuentra en reserva");

                    // Se valida si el cliente tiene una solicitud activa el mismo día
                    var solicitudRegistradaAntigua = await servicio.GetSolicitudCredito(x => 
                                                                                x.IdCliente == solicitudCredito.IdCliente
                                                                                && x.TrackingSolicituds.Where(t => t.Estado.Equals("REGISTRADA") && x.TrackingSolicituds.Count() == 1).Count() > 0
                                                                                && x.FechaElaboracion.CompareTo(solicitudCredito.FechaElaboracion) == 0
                                                                                );

                    if (solicitudRegistradaAntigua != null)
                        return Conflict("No se puede crear más de una solicitud diaria por cliente");

                    var respuesta = await servicio.InsertSolicitudCredito(solicitudCredito);

                    // Se registra el tracking de la solicitud
                    var tracking = new TrackingSolicitud() { IdSolicitud = respuesta.Id, Estado = "REGISTRADA", FechaActualizacion = fechaActualDateOnly };
                    await servicioTra.InsertTrackingSolicitud(tracking);

                    // Se crea la asociación cliente - patio
                    var clientePatio = new ClientePatio() { IdCliente = solicitudCredito.IdCliente, IdPatio = solicitudCredito.IdPatio, FechaAsignacion = fechaActualDateOnly };
                    await servicioCP.InsertClientePatio(clientePatio);

                    return Ok(respuesta);
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
        public async Task<IActionResult> PutSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var solicitudCreditoExiste = await servicio.GetSolicitudCredito(solicitudCredito.Id);

                    if (solicitudCreditoExiste != null)
                        return Ok(await servicio.UpdateSolicitudCredito(solicitudCredito));

                    else
                        return NotFound($"No se encontró el solicitudCredito con Id: {solicitudCredito.Id}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("El modelo de tipo SolicitudCredito está incorrecto");
        }

        [HttpPut]
        [Route("ActualizarSolicitud")]
        public async Task<IActionResult> PutSolicitudCreditoCambioEstado(int IdSolicitud, string Estado)
        {
            var solicitudCreditoExiste = await servicio.GetSolicitudCredito(IdSolicitud);

            if (solicitudCreditoExiste == null)
                return NotFound($"No se encontró el solicitudCredito con Id: {IdSolicitud}");

            var fechaActual = DateTime.Now.Date.ToString().Split(' ')[0];
            var fechaActualDateOnly = DateOnly.Parse(fechaActual);

            // Se registra el tracking de la solicitud
            var tracking = new TrackingSolicitud() { IdSolicitud = IdSolicitud, Estado = Estado, FechaActualizacion = fechaActualDateOnly };
            await servicioTra.InsertTrackingSolicitud(tracking);

            // Se obtiene la solicitud actual
            var solicitud = await servicio.GetSolicitudCredito(x => x.Id == IdSolicitud);

            return Ok(solicitud);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSolicitudCredito(int Id)
        {
            try
            {
                var solicitudCreditoExiste = await servicio.GetSolicitudCredito(Id);

                if (solicitudCreditoExiste != null)
                {
                    // Se elimina los tracking
                    await servicioTra.DeleteTrackingSolicitudMultiple(x => x.IdSolicitud == Id);

                    return Ok(await servicio.DeleteSolicitudCredito(Id));
                }
                else
                    return NotFound($"No se encontró el solicitudCredito con Id: {Id}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


    }
}
