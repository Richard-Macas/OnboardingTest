using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnboardingTest.Entity.Models
{
    public partial class ClientePatio
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdPatio { get; set; }
        public DateOnly FechaAsignacion { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; } = null!;
        public virtual Patio? IdPatioNavigation { get; set; } = null!;
    }
}
