using Microsoft.EntityFrameworkCore;
using OnboardingTest.Domain.Interfaces;
using OnboardingTest.Entity.Models;
using OnboardingTest.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Infrastructure.Services
{
    public class EjecutivoServices: IEjecutivoService
    {
        private readonly CreditoAutomotrizContext _context;

        public EjecutivoServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertMultipleEjecutivo()
        {
            List<Ejecutivo> ejecutivos = File.ReadAllLines($"CSV\\Ejecutivos.csv")
                                           .Skip(1)
                                           .Select(v => EjecutivoParseFromCsv(v))
                                           .ToList();

            // Verificar que no existan repetidos
            var verificarRepetidos = ejecutivos.GroupBy(x => x.Identificacion)
            .Select(x => new {
                Count = x.Count(),
                Identificacion = x.Key,
                Id = x.First().Identificacion
            })
            .OrderByDescending(x => x.Count);

            if (verificarRepetidos.Where(x => x.Count > 1).Count() > 0)
                throw new DuplicateWaitObjectException("Existen registros duplicados en el archivo Ejecutivos.csv");

            // Verificar que no existan en la bdd
            var ejecutivosDB = await _context.Ejecutivos.ToListAsync();

            var ejecutivosYaExistentesDB = ejecutivosDB.Where(x => ejecutivos.Any(c => x.Identificacion == c.Identificacion)).ToList();
            if (ejecutivosYaExistentesDB.Count > 0)
                throw new InvalidOperationException("Uno o más ejecutivos ya se encuentran almacenados en la base de datos");

            _context.Ejecutivos.AddRange(ejecutivos);
            await _context.SaveChangesAsync();

            return true;
        }

        #region Métodos Privados
        private Ejecutivo EjecutivoParseFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            Ejecutivo ejecutivo = new Ejecutivo()
            {
                Identificacion = Convert.ToString(values[0]),
                Nombres = Convert.ToString(values[1]),
                Apellidos = Convert.ToString(values[2]),
                Direccion = Convert.ToString(values[3]),
                TelefonoConvencional = Convert.ToString(values[4]),
                Celular = Convert.ToString(values[5]),
                IdPatio = int.Parse(values[6]),
                Edad = int.Parse(values[7])
            };

            return ejecutivo;
        }
        #endregion
    }
}
