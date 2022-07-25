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
    public class MarcaServices: IMarcaService
    {
        private readonly CreditoAutomotrizContext _context;

        public MarcaServices(CreditoAutomotrizContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertMultipleMarca()
        {
            List<Marca> marcas = File.ReadAllLines($"CSV\\Marcas.csv")
                                           .Skip(1)
                                           .Select(v => MarcaParseFromCsv(v))
                                           .ToList();

            // Verificar que no existan repetidos
            var verificarRepetidos = marcas.GroupBy(x => x.Nombre)
            .Select(x => new {
                Count = x.Count(),
                Nombre = x.Key,
                Id = x.First().Nombre
            })
            .OrderByDescending(x => x.Count);

            if (verificarRepetidos.Where(x => x.Count > 1).Count() > 0)
                throw new DuplicateWaitObjectException("Existen registros duplicados en el archivo Marcas.csv");

            // Verificar que no existan en la bdd
            var marcasDB = await _context.Marcas.ToListAsync();

            var marcasYaExistentesDB = marcasDB.Where(x => marcas.Any(c => x.Nombre == c.Nombre)).ToList();
            if (marcasYaExistentesDB.Count > 0)
                throw new InvalidOperationException("Uno o más marcas ya se encuentran almacenados en la base de datos");

            _context.Marcas.AddRange(marcas);
            await _context.SaveChangesAsync();

            return true;
        }

        #region Métodos Privados
        private Marca MarcaParseFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            Marca marca = new Marca();
            marca.Nombre = Convert.ToString(values[0]);

            return marca;
        }
        #endregion
    }
}
