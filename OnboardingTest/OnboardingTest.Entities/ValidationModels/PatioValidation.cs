using FluentValidation;
using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Entity.ValidationModels
{
    public class PatioValidation : AbstractValidator<Patio>
    {
        public PatioValidation()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull();
            RuleFor(x => x.Direccion).NotEmpty().NotNull();
            RuleFor(x => x.Telefono).NotEmpty().NotNull();
            RuleFor(x => x.NumeroPuntoVenta).NotEmpty().NotNull();
        }
    }
}
