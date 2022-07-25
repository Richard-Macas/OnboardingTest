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
    public class TestVehiculoController
    {
        private readonly VehiculoController _vehiculoController;
        private readonly IVehiculoService _servicio;
        private readonly CreditoAutomotrizContext _context;

        public TestVehiculoController()
        {
            _context = new CreditoAutomotrizContext();
            _servicio = new VehiculoServices(_context);
            _vehiculoController = new VehiculoController(_servicio);
        }

        [Fact]
        public async Task GetVehiculos_OnSuccess_ReturnsOKResult()
        {

            // Act
            var okMarcaResult = (OkObjectResult)await _vehiculoController.GetVehiculos("kia");
            var okModeloResult = (OkObjectResult)await _vehiculoController.GetVehiculos("Cilan");
            var okAnioResult = (OkObjectResult)await _vehiculoController.GetVehiculos("2017");

            // Assert
            Assert.IsType<OkObjectResult>(okMarcaResult);
            Assert.IsType<OkObjectResult>(okModeloResult);
            Assert.IsType<OkObjectResult>(okAnioResult);
        }

        [Fact]
        public async Task GetVehiculo_UnknownId_ReturnsNotFoundResult()
        {

            // Act
            var notFoundResult = (NotFoundResult)await _vehiculoController.GetVehiculo(100);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetVehiculo_ExistingId_ReturnsOkResult()
        {

            // Act
            var okResult = (OkObjectResult)await _vehiculoController.GetVehiculo(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

       [Fact]
        public async Task PostVehiculo_BadModel_ReturnBadRequest()
        {
            
            // Act
            var badRequestResult = (BadRequestObjectResult)await _vehiculoController.PostVehiculo(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task PostVehiculo_Duplicate_ReturnConfictResult()
        {
            // Arrange
            var vehiculo = new Vehiculo()
            {
                Placa = "PBA-741",
                Modelo = "Gen1",
                NroChasis = 258,
                IdMarca = 3,
                Cilindraje = 15,
                Avaluo = 58,
                Anio = 2012
            };

            // Act
            var conflictResult = (ConflictObjectResult)await _vehiculoController.PostVehiculo(vehiculo);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task PostVehiculo_ValidModel_ReturnOkResult()
        {
            // Arrange
            var vehiculo = new Vehiculo()
            {
                Placa = "PCT-698",
                Modelo = "Gen1",
                NroChasis = 258,
                IdMarca = 3,
                Cilindraje = 15,
                Avaluo = 58,
                Anio = 2012
            };

            // Act
            var okResult = (OkObjectResult)await _vehiculoController.PostVehiculo(vehiculo);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        
        [Fact]
        public async Task PutVehiculo_BadModel_ReturnBadRequest()
        {

            // Act
            var badRequestResult = (BadRequestObjectResult)await _vehiculoController.PutVehiculo(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);

        }

        [Fact]
        public async Task PutVehiculo_NotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var vehiculo = new Vehiculo()
            {
                Id = 100,
                Placa = "PCT-698",
                Modelo = "Gen1",
                NroChasis = 258,
                IdMarca = 3,
                Cilindraje = 15,
                Avaluo = 58,
                Anio = 2012
            };

            // Act
            var notFoundResult = (NotFoundObjectResult)await _vehiculoController.PutVehiculo(vehiculo);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task PutVehiculo_Duplicate_ReturnConfictResult()
        {
            // Arrange
            var vehiculo = new Vehiculo()
            {
                Id=2,
                Placa = "PCT-698",
                Modelo = "Gen1",
                NroChasis = 258,
                IdMarca = 3,
                Cilindraje = 15,
                Avaluo = 58,
                Anio = 2012
            };

            // Act
            var conflictResult = (ConflictObjectResult)await _vehiculoController.PutVehiculo(vehiculo);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }
        
        [Fact]
        public async Task PutVehiculo_ValidModel_ReturnOkResult()
        {
            // Arrange
            var vehiculo = new Vehiculo()
            {
                Id = 5,
                Placa = "PCT-698",
                Modelo = "Gen2",
                NroChasis = 258,
                IdMarca = 3,
                Cilindraje = 15,
                Avaluo = 58,
                Anio = 2012
            };

            // Act
            var okResult = (OkObjectResult)await _vehiculoController.PutVehiculo(vehiculo);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        
        [Fact]
        public async Task DeleteVehiculo_UnknownId_ReturnNoFoundResponse()
        {
            // Act
            var notFoundResult = (NotFoundObjectResult) await _vehiculoController.DeleteVehiculo(100);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteVehiculo_ConflitRelationShip_ReturnConflictResult()
        {
            // Act
            var conflictResult = (ConflictObjectResult)await _vehiculoController.DeleteVehiculo(21);

            // Assert
            Assert.IsType<ConflictObjectResult>(conflictResult);
        }

        [Fact]
        public async Task DeleteVehiculo_ExistingId_ReturnOkResult()
        {
            // Act
            var okResult = (OkObjectResult)await _vehiculoController.DeleteVehiculo(5);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
