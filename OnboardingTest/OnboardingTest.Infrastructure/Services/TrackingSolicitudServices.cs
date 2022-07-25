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
    public class TrackingSolicitudServices : ITrackingSolicitudService
    {
        private readonly CreditoAutomotrizContext _context;

        public TrackingSolicitudServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrackingSolicitud>> GetTrackingSolicituds()
        {
            return await _context.TrackingSolicituds.ToListAsync();
        }

        public async Task<TrackingSolicitud?> GetTrackingSolicitud(int Id)
        {
            var trackingSolicitud = await _context.TrackingSolicituds.FindAsync(Id);
            if (trackingSolicitud != null)
                _context.Entry(trackingSolicitud).State = EntityState.Detached;

            return trackingSolicitud;
        }

        public async Task<TrackingSolicitud?> GetTrackingSolicitud(Expression<Func<TrackingSolicitud, bool>> expr)
        {
             var trackingSolicitud = (await _context.TrackingSolicituds.Where(expr).ToListAsync()).FirstOrDefault();

            if (trackingSolicitud != null)
                _context.Entry(trackingSolicitud).State = EntityState.Detached;

            return trackingSolicitud;
        }

        public async Task<TrackingSolicitud> InsertTrackingSolicitud(TrackingSolicitud trackingSolicitud)
        {
            var trackingSolicitudInsert = _context.TrackingSolicituds.Add(trackingSolicitud);
            await _context.SaveChangesAsync();
            return trackingSolicitudInsert.Entity;
        }

        public async Task<TrackingSolicitud> UpdateTrackingSolicitud(TrackingSolicitud trackingSolicitud)
        {
            _context.Entry(trackingSolicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return trackingSolicitud;
        }

        public async Task<bool> DeleteTrackingSolicitud(int Id)
        {
            var trackingSolicitud = _context.TrackingSolicituds.Find(Id);
            if (trackingSolicitud != null)
            {
                _context.Set<TrackingSolicitud>().Remove(trackingSolicitud);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<TrackingSolicitud>> DeleteTrackingSolicitudMultiple(Expression<Func<TrackingSolicitud, bool>> expr)
        {
            var trackings = await _context.TrackingSolicituds.Where(expr).ToListAsync();

            _context.Set<TrackingSolicitud>().RemoveRange(trackings);
            await _context.SaveChangesAsync();

            return trackings;

        }

    }
}
