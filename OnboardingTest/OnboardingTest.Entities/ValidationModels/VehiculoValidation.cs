using FluentValidation;
using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Entity.ValidationModels
{
    public class VehiculoValidation : AbstractValidator<Vehiculo>
    {
        public VehiculoValidation()
        {
            RuleFor(x => x.Placa).NotEmpty().NotNull();
            RuleFor(x => x.Modelo).NotEmpty().NotNull();
            RuleFor(x => x.NroChasis).NotEmpty().NotNull();
            RuleFor(x => x.IdMarca).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Cilindraje).NotEmpty().NotNull();
            RuleFor(x => x.Avaluo).NotEmpty().NotNull();
            RuleFor(x => x.Anio).NotEmpty().NotNull();
        }
    }
}
