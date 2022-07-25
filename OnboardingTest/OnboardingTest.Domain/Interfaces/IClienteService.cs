using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente?> GetCliente(int Id);
        Task<Cliente?> GetCliente(Expression<Func<Cliente, bool>> expr);
        Task<Cliente> InsertCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(int Id);
        Task<bool> InsertMultipleCliente();
    }
}
