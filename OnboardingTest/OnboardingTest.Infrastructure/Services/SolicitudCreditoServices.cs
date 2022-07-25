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
    public class SolicitudCreditoServices : ISolicitudCreditoService
    {
        private readonly CreditoAutomotrizContext _context;

        public SolicitudCreditoServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolicitudCredito>> GetSolicitudCreditos()
        {
            return await _context.SolicitudCreditos.ToListAsync();
        }

        public async Task<SolicitudCredito> GetSolicitudCredito(int Id)
        {
            var solicitudCredito = await _context.SolicitudCreditos.FindAsync(Id);
            if (solicitudCredito != null)
                _context.Entry(solicitudCredito).State = EntityState.Detached;

            return solicitudCredito;
        }

        public async Task<SolicitudCredito> GetSolicitudCredito(Expression<Func<SolicitudCredito, bool>> expr)
        {
             var solicitudCredito = (await _context.SolicitudCreditos.Where(expr)
                .Include(x => x.IdVehiculoNavigation)
                .Include(x => x.TrackingSolicituds)
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdEjecutivoNavigation)
                .Include(x => x.IdPatioNavigation)
                .ToListAsync()).FirstOrDefault();

            if (solicitudCredito != null)
                _context.Entry(solicitudCredito).State = EntityState.Detached;

            return solicitudCredito;
        }

        public async Task<SolicitudCredito> InsertSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            var solicitudCreditoInsert = _context.SolicitudCreditos.Add(solicitudCredito);
            await _context.SaveChangesAsync();
            return solicitudCreditoInsert.Entity;
        }

        public async Task<SolicitudCredito> UpdateSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            _context.Entry(solicitudCredito).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return solicitudCredito;
        }

        public async Task<bool> DeleteSolicitudCredito(int Id)
        {
            var solicitudCredito = _context.SolicitudCreditos.Find(Id);
            if (solicitudCredito != null)
            {
                _context.Set<SolicitudCredito>().Remove(solicitudCredito);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
