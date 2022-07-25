using FluentValidation;
using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Entity.ValidationModels
{
    public class ClientePatioValidation : AbstractValidator<ClientePatio>
    {
        public ClientePatioValidation()
        {
            RuleFor(x => x.IdCliente).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.IdPatio).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.FechaAsignacion).NotNull();
        }
    }
}
