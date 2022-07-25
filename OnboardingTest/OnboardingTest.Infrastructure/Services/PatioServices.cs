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
    public class PatioServices : IPatioService
    {
        private readonly CreditoAutomotrizContext _context;

        public PatioServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patio>> GetPatios()
        {
            return await _context.Patios.ToListAsync();
        }

        public async Task<Patio?> GetPatio(int Id)
        {
            var patio = await _context.Patios.FindAsync(Id);
            if (patio != null)
                _context.Entry(patio).State = EntityState.Detached;

            return patio;
        }

        public async Task<Patio?> GetPatio(Expression<Func<Patio, bool>> expr)
        {
             var patio = (await _context.Patios.Where(expr).ToListAsync()).FirstOrDefault();

            if (patio != null)
                _context.Entry(patio).State = EntityState.Detached;

            return patio;
        }

        public async Task<Patio> InsertPatio(Patio patio)
        {
            var patioInsert = _context.Patios.Add(patio);
            await _context.SaveChangesAsync();
            return patioInsert.Entity;
        }

        public async Task<Patio> UpdatePatio(Patio patio)
        {
            _context.Entry(patio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return patio;
        }

        public async Task<bool> DeletePatio(int Id)
        {
            var patio = _context.Patios.Find(Id);
            if (patio != null)
            {
                _context.Set<Patio>().Remove(patio);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
