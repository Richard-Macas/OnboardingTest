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
    public class ClientePatioServices : IClientePatioService
    {
        private readonly CreditoAutomotrizContext _context;

        public ClientePatioServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientePatio>> GetClientePatios()
        {
            return await _context.ClientePatios
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdPatioNavigation)
                .ToListAsync();
        }

        public async Task<ClientePatio> GetClientePatio(int Id)
        {
            var clientePatio = await _context.ClientePatios.Where(x => x.Id == Id)
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdPatioNavigation)
                .FirstOrDefaultAsync();
            if (clientePatio != null)
                _context.Entry(clientePatio).State = EntityState.Detached;

            return clientePatio;
        }

        public async Task<ClientePatio> GetClientePatio(Expression<Func<ClientePatio, bool>> expr)
        {
             var clientePatio = (await _context.ClientePatios.Where(expr)
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdPatioNavigation).ToListAsync()).FirstOrDefault();

            if (clientePatio != null)
                _context.Entry(clientePatio).State = EntityState.Detached;

            return clientePatio;
        }

        public async Task<ClientePatio> InsertClientePatio(ClientePatio clientePatio)
        {
            var clientePatioInsert = _context.ClientePatios.Add(clientePatio);
            await _context.SaveChangesAsync();
            return clientePatioInsert.Entity;
        }

        public async Task<ClientePatio> UpdateClientePatio(ClientePatio clientePatio)
        {
            _context.Entry(clientePatio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return clientePatio;
        }

        public async Task<bool> DeleteClientePatio(int Id)
        {
            var clientePatio = _context.ClientePatios.Find(Id);
            if (clientePatio != null)
            {
                _context.Set<ClientePatio>().Remove(clientePatio);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
