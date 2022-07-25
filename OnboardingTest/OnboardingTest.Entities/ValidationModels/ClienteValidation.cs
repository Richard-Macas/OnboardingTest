using FluentValidation;
using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Entity.ValidationModels
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(x => x.Identificacion).NotEmpty().NotNull();
            RuleFor(x => x.Nombres).NotEmpty().NotNull();
            RuleFor(x => x.Apellidos).NotEmpty().NotNull();
            RuleFor(x => x.Edad).GreaterThan(0);
            RuleFor(x => x.FechaNacimiento).NotNull();
            RuleFor(x => x.Direccion).NotEmpty().NotNull();
            RuleFor(x => x.EstadoCivil).NotEmpty().NotNull();
            RuleFor(x => x.IdentificacionConyugue).NotEmpty().NotNull();
            RuleFor(x => x.NombreConyugue).NotEmpty().NotNull();
            RuleFor(x => x.Telefono).NotEmpty().NotNull();
        }
    }
}
