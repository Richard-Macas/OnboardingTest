using Microsoft.EntityFrameworkCore;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;
using OnboardingTest.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Infrastructure.Services
{
    public class VehiculoServices : IVehiculoService
    {
        private readonly CreditoAutomotrizContext _context;

        public VehiculoServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> GetVehiculos()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<IEnumerable<Vehiculo>> GetVehiculos(Expression<Func<Vehiculo, bool>> expr)
        {
            return await _context.Vehiculos.Where(expr).ToListAsync();
        }

        public async Task<Vehiculo> GetVehiculo(int Id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(Id);
            if (vehiculo != null)
                _context.Entry(vehiculo).State = EntityState.Detached;

            return vehiculo;
        }

        public async Task<Vehiculo> GetVehiculo(Expression<Func<Vehiculo, bool>> expr)
        {
             var vehiculo = (await _context.Vehiculos.Where(expr).ToListAsync()).FirstOrDefault();

            if (vehiculo != null)
                _context.Entry(vehiculo).State = EntityState.Detached;

            return vehiculo;
        }

        public async Task<Vehiculo> InsertVehiculo(Vehiculo vehiculo)
        {
            var vehiculoInsert = _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            return vehiculoInsert.Entity;
        }

        public async Task<Vehiculo> UpdateVehiculo(Vehiculo vehiculo)
        {
            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return vehiculo;
        }

        public async Task<bool> DeleteVehiculo(int Id)
        {
            var vehiculo = _context.Vehiculos.Find(Id);
            if (vehiculo != null)
            {
                _context.Set<Vehiculo>().Remove(vehiculo);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
