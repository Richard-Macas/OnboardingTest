using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface IVehiculoService
    {
        Task<IEnumerable<Vehiculo>> GetVehiculos();
        Task<IEnumerable<Vehiculo>> GetVehiculos(Expression<Func<Vehiculo, bool>> expr);
        Task<Vehiculo?> GetVehiculo(int Id);
        Task<Vehiculo?> GetVehiculo(Expression<Func<Vehiculo, bool>> expr);
        Task<Vehiculo> InsertVehiculo(Vehiculo vehiculo);
        Task<Vehiculo> UpdateVehiculo(Vehiculo vehiculo);
        Task<bool> DeleteVehiculo(int Id);
    }
}
