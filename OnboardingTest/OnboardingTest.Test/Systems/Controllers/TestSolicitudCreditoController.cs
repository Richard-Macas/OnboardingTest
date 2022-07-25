using Microsoft.AspNetCore.Mvc;
using Moq;
using OnboardingTest.API.Controllers;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;
using OnboardingTest.Infrastructure.Services;
using OnboardingTest.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Test.Systems.Controllers
{
    public class TestSolicitudCreditoController
    {
        private readonly SolicitudCreditoController _solicitudCreditoController;
        private readonly ISolicitudCreditoService _servicio;
        private readonly ITrackingSolicitudService _servicioTra;
        private readonly IClientePatioService _servicioCP;
        private readonly CreditoAutomotrizContext _context;

        public TestSolicitudCreditoController()
        {
            _context = new CreditoAutomotrizContext();
            _servicio = new SolicitudCreditoServices(_context);
            _servicioTra = new TrackingSolicitudServices(_context);
            _servicioCP = new ClientePatioServices(_context);
            _solicitudCreditoController = new SolicitudCreditoController(_servicio, _servicioTra, _servicioCP);
            
        }

        [Fact]
        public async Task GetSolicitudCreditos_OnSuccess_ReturnsOKResult()
        {

            // Act
            var okResult = (OkObjectResult)await _solicitudCreditoController.GetSolicitudCreditos();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetSolicitudCredito_UnknownId_ReturnsNotFoundResult()
        {

            // Act
            var notFoundResult = (NotFoundResult)await _solicitudCreditoController.GetSolicitudCredito(100);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetSolicitudCredito_ExistingId_ReturnsOkResult()
        {

            // Act
            var okResult = (OkObjectResult)await _solicitudCreditoController.GetSolicitudCredito(4);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PostSolicitudCredito_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _solicitudCreditoController.PostSolicitudCredito(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task PostSolicitudCredito_SameCarActive_ReturnConflictResult ()
        {
            // Arrange
            var solicitudCredito = new SolicitudCredito()
            {
                IdCliente = 23,
                IdPatio = 1,
                IdVehiculo = 6,
                FechaElaboracion = DateOnly.Parse("2022/07/24"),
                MesesPlazo = 12,
                Cuotas = 450,
                Entrada = 300,
                IdEjecutivo = 1,
            };

            // Act
            var conflictResult = (ConflictObjectResult)await _solicitudCreditoController.PostSolicitudCredito(solicitudCredito);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task PostSolicitudCredito_SameClientDay_ReturnConflictResult()
        {
            // Arrange
            var solicitudCredito = new SolicitudCredito()
            {
                IdCliente = 21,
                IdPatio = 1,
                IdVehiculo = 1,
                FechaElaboracion = DateOnly.Parse("2022/07/24"),
                MesesPlazo = 12,
                Cuotas = 450,
                Entrada = 300,
                IdEjecutivo = 1,
            };

            // Act
            var conflictResult = (ConflictObjectResult)await _solicitudCreditoController.PostSolicitudCredito(solicitudCredito);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task PostSolicitudCredito_ValidModel_ReturnOkResult()
        {
            // Arrange
            var solicitudCredito = new SolicitudCredito()
            {
                IdCliente = 30,
                IdPatio = 1,
                IdVehiculo = 7,
                FechaElaboracion = DateOnly.Parse("2022/07/24"),
                MesesPlazo = 12,
                Cuotas = 450,
                Entrada = 300,
                IdEjecutivo = 1,
            };

            // Act
            var okResult = (OkObjectResult)await _solicitudCreditoController.PostSolicitudCredito(solicitudCredito);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PutSolicitudCredito_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _solicitudCreditoController.PutSolicitudCredito(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);

        }

        [Fact]
        public async Task PutSolicitudCredito_NotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var solicitudCredito = new SolicitudCredito()
            {
                Id = 50,
                IdCliente = 30,
                IdPatio = 1,
                IdVehiculo = 7,
                FechaElaboracion = DateOnly.Parse("2022/07/24"),
                MesesPlazo = 12,
                Cuotas = 450,
                Entrada = 300,
                IdEjecutivo = 1,
            };

            // Act
            var notFoundResult = (NotFoundObjectResult)await _solicitudCreditoController.PutSolicitudCredito(solicitudCredito);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task PutSolicitudCredito_ValidModel_ReturnOkResult()
        {
            // Arrange
            var solicitudCredito = new SolicitudCredito()
            {
                Id = 6,
                IdCliente = 30,
                IdPatio = 1,
                IdVehiculo = 7,
                FechaElaboracion = DateOnly.Parse("2022/07/25"),
                MesesPlazo = 12,
                Cuotas = 450,
                Entrada = 300,
                IdEjecutivo = 1,
            };

            // Act
            var okResult = (OkObjectResult)await _solicitudCreditoController.PutSolicitudCredito(solicitudCredito);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PutSolicitudCredito_UpdateStateSolicitudUnknownId_ReturnNotFoundResponse()
        {

            // Act
            var notFoundResult = (NotFoundObjectResult)await _solicitudCreditoController.PutSolicitudCreditoCambioEstado(17, "CANCELADA");

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task PutSolicitudCredito_UpdateStateSolicitudExistingId_ReturnNotFoundResponse()
        {

            // Act
            var okResult = (OkObjectResult)await _solicitudCreditoController.PutSolicitudCreditoCambioEstado(7, "CANCELADA");

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task DeleteSolicitudCredito_UnknownId_ReturnNoFoundResponse()
        {
            // Act
            var notFoundResult = (NotFoundObjectResult) await _solicitudCreditoController.DeleteSolicitudCredito(100);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteSolicitudCredito_ConflitRelationShip_ReturnConflictResult()
        {
            // Act
            var conflictResult = (ConflictObjectResult)await _solicitudCreditoController.DeleteSolicitudCredito(3);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task DeleteSolicitudCredito_ExistingIdSecuence_ReturnOkResult()
        {
            // Act
            var okResult = (OkObjectResult)await _solicitudCreditoController.DeleteSolicitudCredito(7);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
