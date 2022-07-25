using FluentValidation;
using OnboardingTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingTest.Entity.ValidationModels
{
    public class SolicitudCreditoValidation : AbstractValidator<SolicitudCredito>
    {
        public SolicitudCreditoValidation()
        {
            RuleFor(x => x.FechaElaboracion).NotNull();
            RuleFor(x => x.IdCliente).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.IdPatio).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.IdVehiculo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.MesesPlazo).NotEmpty().NotNull();
            RuleFor(x => x.Cuotas).NotEmpty().NotNull();
            RuleFor(x => x.Entrada).NotEmpty().NotNull();
            RuleFor(x => x.IdEjecutivo).NotEmpty().NotNull();
        }
    }
}
