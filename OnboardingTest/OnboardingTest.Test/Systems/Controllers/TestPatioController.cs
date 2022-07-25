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
    public class TestPatioController
    {
        private readonly PatioController _patioController;
        private readonly IPatioService _servicio;
        private readonly CreditoAutomotrizContext _context;

        public TestPatioController()
        {
            _context = new CreditoAutomotrizContext();
            _servicio = new PatioServices(_context);
            _patioController = new PatioController(_servicio);
        }

        [Fact]
        public async Task GetPatios_OnSuccess_ReturnsOKResult()
        {

            // Act
            var okResult = (OkObjectResult)await _patioController.GetPatios();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetPatio_UnknownId_ReturnsNotFoundResult()
        {

            // Act
            var notFoundResult = (NotFoundResult)await _patioController.GetPatio(100);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetPatio_ExistingId_ReturnsOkResult()
        {

            // Act
            var okResult = (OkObjectResult)await _patioController.GetPatio(7);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PostPatio_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _patioController.PostPatio(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task PostPatio_ValidModel_ReturnOkResult()
        {
            // Arrange
            var patio = new Patio()
            {
                Nombre = "Patio Ruedas",
                Direccion = "Av. Las Marianas",
                Telefono = "022000000",
                NumeroPuntoVenta = 700
            };

            // Act
            var okResult = (OkObjectResult)await _patioController.PostPatio(patio);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PutPatio_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _patioController.PutPatio(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);

        }

        [Fact]
        public async Task PutPatio_NotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var patio = new Patio()
            {
                Id = 100,
                Nombre = "Patio Tuercas",
                Direccion = "Av. Chillanes",
                Telefono = "0950000000",
                NumeroPuntoVenta = 300
            };

            // Act
            var notFoundResult = (NotFoundObjectResult)await _patioController.PutPatio(patio);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task PutPatio_ValidModel_ReturnOkResult()
        {
            // Arrange
            var patio = new Patio()
            {
                Id = 9,
                Nombre = "Patio Tuercas",
                Direccion = "Av. Chillanes",
                Telefono = "022000000",
                NumeroPuntoVenta = 300
            };

            // Act
            var okResult = (OkObjectResult)await _patioController.PutPatio(patio);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task DeletePatio_UnknownId_ReturnNoFoundResponse()
        {
            // Act
            var notFoundResult = (NotFoundObjectResult) await _patioController.DeletePatio(100);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeletePatio_ConflitRelationShip_ReturnConflictResult()
        {
            // Act
            var conflictResult = (ConflictObjectResult)await _patioController.DeletePatio(1);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task DeletePatio_ExistingId_ReturnOkResult()
        {
            // Act
            var okResult = (OkObjectResult)await _patioController.DeletePatio(10);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
