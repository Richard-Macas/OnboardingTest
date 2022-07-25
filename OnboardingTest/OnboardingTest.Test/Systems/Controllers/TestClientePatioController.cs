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
    public class TestClientePatioController
    {
        private readonly ClientePatioController _clientePatioController;
        private readonly IClientePatioService _servicio;
        private readonly CreditoAutomotrizContext _context;

        public TestClientePatioController()
        {
            _context = new CreditoAutomotrizContext();
            _servicio = new ClientePatioServices(_context);
            _clientePatioController = new ClientePatioController(_servicio);
        }

        [Fact]
        public async Task GetClientePatios_OnSuccess_ReturnsOKResult()
        {

            // Act
            var okResult = (OkObjectResult)await _clientePatioController.GetClientePatios();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetClientePatio_UnknownId_ReturnsNotFoundResult()
        {

            // Act
            var notFoundResult = (NotFoundResult)await _clientePatioController.GetClientePatio(100);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetClientePatio_ExistingId_ReturnsOkResult()
        {

            // Act
            var okResult = (OkObjectResult)await _clientePatioController.GetClientePatio(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PostClientePatio_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _clientePatioController.PostClientePatio(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task PostClientePatio_ValidModel_ReturnOkResult()
        {
            // Arrange
            var clientePatio = new ClientePatio()
            {
                IdCliente = 25,
                IdPatio = 5,
                FechaAsignacion = DateOnly.Parse("01/01/2015")
            };

            // Act
            var okResult = (OkObjectResult)await _clientePatioController.PostClientePatio(clientePatio);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PutClientePatio_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _clientePatioController.PutClientePatio(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);

        }

        [Fact]
        public async Task PutClientePatio_NotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var clientePatio = new ClientePatio()
            {
                Id = 14,
                IdCliente = 25,
                IdPatio = 5,
                FechaAsignacion = DateOnly.Parse("01/01/2015")
            };

            // Act
            var notFoundResult = (NotFoundObjectResult)await _clientePatioController.PutClientePatio(clientePatio);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task PutClientePatio_ValidModel_ReturnOkResult()
        {
            // Arrange
            var clientePatio = new ClientePatio()
            {
                Id = 4,
                IdCliente = 25,
                IdPatio = 5,
                FechaAsignacion = DateOnly.Parse("01/01/2015")
            };

            // Act
            var okResult = (OkObjectResult)await _clientePatioController.PutClientePatio(clientePatio);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task DeleteClientePatio_UnknownId_ReturnNoFoundResponse()
        {
            // Act
            var notFoundResult = (NotFoundObjectResult) await _clientePatioController.DeleteClientePatio(100);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteClientePatio_ExistingId_ReturnOkResult()
        {
            // Act
            var okResult = (OkObjectResult)await _clientePatioController.DeleteClientePatio(2);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        
    }
}
