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
    public class TestClienteController
    {
        private readonly ClienteController _clienteController;
        private readonly IClienteService _servicio;
        private readonly CreditoAutomotrizContext _context;

        public TestClienteController()
        {
            _context = new CreditoAutomotrizContext();
            _servicio = new ClienteServices(_context);
            _clienteController = new ClienteController(_servicio);
        }

        [Fact]
        public async Task GetClientes_OnSuccess_ReturnsOKResult()
        {

            // Act
            var okResult = (OkObjectResult)await _clienteController.GetClientes();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetCliente_UnknownId_ReturnsNotFoundResult()
        {

            // Act
            var notFoundResult = (NotFoundResult)await _clienteController.GetCliente(100);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetCliente_ExistingId_ReturnsOkResult()
        {

            // Act
            var okResult = (OkObjectResult)await _clienteController.GetCliente(21);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PostCliente_BadModel_ReturnBadRequest()
        {
            // Arrange
            var cliente = new Cliente()
            {
                Nombres = "Pepito",
                Apellidos = "Perez",
                Edad = -1
            };

            // Act
            var badRequestResult = (BadRequestObjectResult)await _clienteController.PostCliente(cliente);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task PostCliente_Duplicate_ReturnConfictResult()
        {
            // Arrange
            var cliente = new Cliente()
            {
                Identificacion = "1000000000",
                Nombres = "Pepito",
                Apellidos = "Perez",
                Edad = 25,
                FechaNacimiento = DateOnly.Parse("01/01/1978"),
                Direccion = "Av. República",
                Telefono = "022000000",
                EstadoCivil = "Soltero",
                IdentificacionConyugue = "1700000000",
                NombreConyugue = "Sara Silvarnos",
                SujetoCredito = true
            };

            // Act
            var conflictResult = (ConflictObjectResult)await _clienteController.PostCliente(cliente);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task PostCliente_ValidModel_ReturnOkResult()
        {
            // Arrange
            var cliente = new Cliente()
            {
                Identificacion = "1000020000",
                Nombres = "Pepito",
                Apellidos = "Perez",
                Edad = 25,
                FechaNacimiento = DateOnly.Parse("01/01/1978"),
                Direccion = "Av. República",
                Telefono = "022000000",
                EstadoCivil = "Soltero",
                IdentificacionConyugue = "1700000000",
                NombreConyugue = "Sara Silvarnos",
                SujetoCredito = true
            };

            // Act
            var okResult = (OkObjectResult)await _clienteController.PostCliente(cliente);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PutCliente_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _clienteController.PutCliente(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);

        }

        [Fact]
        public async Task PutCliente_NotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var cliente = new Cliente()
            {
                Id = 100,
                Identificacion = "1000060000",
                Nombres = "Pepito",
                Apellidos = "Perez",
                Edad = 25,
                FechaNacimiento = DateOnly.Parse("01/01/1978"),
                Direccion = "Av. República",
                Telefono = "022000000",
                EstadoCivil = "Soltero",
                IdentificacionConyugue = "1700000000",
                NombreConyugue = "Sara Silvarnos",
                SujetoCredito = true
            };

            // Act
            var notFoundResult = (NotFoundObjectResult)await _clienteController.PutCliente(cliente);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task PutCliente_Duplicate_ReturnConfictResult()
        {
            // Arrange
            var cliente = new Cliente()
            {
                Id=21,
                Identificacion = "1000000001",
                Nombres = "Pepito",
                Apellidos = "Perez",
                Edad = 25,
                FechaNacimiento = DateOnly.Parse("01/01/1978"),
                Direccion = "Av. República",
                Telefono = "022000000",
                EstadoCivil = "Soltero",
                IdentificacionConyugue = "1700000000",
                NombreConyugue = "Sara Silvarnos",
                SujetoCredito = true
            };

            // Act
            var conflictResult = (ConflictObjectResult)await _clienteController.PutCliente(cliente);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task PutCliente_ValidModel_ReturnOkResult()
        {
            // Arrange
            var cliente = new Cliente()
            {
                Id = 21,
                Identificacion = "1000020000",
                Nombres = "Pepito",
                Apellidos = "Perez",
                Edad = 25,
                FechaNacimiento = DateOnly.Parse("01/01/1978"),
                Direccion = "Av. República",
                Telefono = "022000000",
                EstadoCivil = "Soltero",
                IdentificacionConyugue = "1700000000",
                NombreConyugue = "Sara Silvarnos",
                SujetoCredito = true
            };

            // Act
            var okResult = (OkObjectResult)await _clienteController.PutCliente(cliente);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task DeleteCliente_UnknownId_ReturnNoFoundResponse()
        {
            // Act
            var notFoundResult = (NotFoundObjectResult) await _clienteController.DeleteCliente(100);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteCliente_ConflitRelationShip_ReturnConflictResult()
        {
            // Act
            var conflictResult = (ConflictObjectResult)await _clienteController.DeleteCliente(21);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task DeleteCliente_ExistingId_ReturnOkResult()
        {
            // Act
            var okResult = (OkObjectResult)await _clienteController.DeleteCliente(46);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
