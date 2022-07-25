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
    public class ClienteServices : IClienteService
    {
        private readonly CreditoAutomotrizContext _context;

        public ClienteServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetCliente(int Id)
        {
            var cliente = await _context.Clientes.FindAsync(Id);
            if (cliente != null)
                _context.Entry(cliente).State = EntityState.Detached;

            return cliente;
        }

        public async Task<Cliente?> GetCliente(Expression<Func<Cliente, bool>> expr)
        {
             var cliente = (await _context.Clientes.Where(expr).ToListAsync()).FirstOrDefault();

            if (cliente != null)
                _context.Entry(cliente).State = EntityState.Detached;

            return cliente;
        }

        public async Task<Cliente> InsertCliente(Cliente cliente)
        {
            var clienteInsert = _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return clienteInsert.Entity;
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<bool> DeleteCliente(int Id)
        {
            var cliente = _context.Clientes.Find(Id);
            if (cliente != null)
            {
                _context.Set<Cliente>().Remove(cliente);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> InsertMultipleCliente()
        {
            List<Cliente> clientes = File.ReadAllLines($"CSV\\Clientes.csv")
                                           .Skip(1)
                                           .Select(v => ClienteParseFromCsv(v))
                                           .ToList();

            // Verificar que no existan repetidos
            var verificarRepetidos = clientes.GroupBy(x => x.Identificacion)
            .Select(x => new
            {
                Count = x.Count(),
                Identificacion = x.Key,
                Id = x.First().Identificacion
            })
            .OrderByDescending(x => x.Count);

            if (verificarRepetidos.Where(x => x.Count > 1).Count() > 0)
                throw new DuplicateWaitObjectException("Existen registros duplicados en el archivo Clientes.csv");

            // Verificar que no existan en la bdd
            var clientesDB = await _context.Clientes.ToListAsync();

            var clientesYaExistentesDB = clientesDB.Where(x => clientes.Any(c => x.Identificacion == c.Identificacion)).ToList();
            if (clientesYaExistentesDB.Count > 0)
                throw new InvalidOperationException("Uno o más clientes ya se encuentran almacenados en la base de datos");

            _context.Clientes.AddRange(clientes);
            await _context.SaveChangesAsync();

            return true;
        }

        #region Métodos Privados
        private Cliente ClienteParseFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            Cliente cliente = new Cliente()
            {
                Identificacion = Convert.ToString(values[0]),
                Nombres = Convert.ToString(values[1]),
                Apellidos = Convert.ToString(values[2]),
                Edad = Convert.ToInt32(values[3]),
                FechaNacimiento = DateOnly.Parse(values[4]),
                Direccion = Convert.ToString(values[5]),
                EstadoCivil = Convert.ToString(values[6]),
                IdentificacionConyugue = Convert.ToString(values[7]),
                NombreConyugue = Convert.ToString(values[8]),
                SujetoCredito = Convert.ToBoolean(values[9]),
                Telefono = Convert.ToString(values[10]),
            };

            return cliente;
        }
        #endregion
    }
}
