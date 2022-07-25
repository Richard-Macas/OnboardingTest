using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface IClientePatioService
    {
        Task<IEnumerable<ClientePatio>> GetClientePatios();
        Task<ClientePatio> GetClientePatio(int Id);
        Task<ClientePatio> GetClientePatio(Expression<Func<ClientePatio, bool>> expr);
        Task<ClientePatio> InsertClientePatio(ClientePatio clientePatio);
        Task<ClientePatio> UpdateClientePatio(ClientePatio clientePatio);
        Task<bool> DeleteClientePatio(int Id);
    }
}
