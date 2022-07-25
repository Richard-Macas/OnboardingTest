using System;
using System.Collections.Generic;

namespace OnboardingTest.Entity.Models
{
    public partial class Ejecutivo
    {
        public Ejecutivo()
        {
            SolicitudCreditos = new HashSet<SolicitudCredito>();
        }

        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string? TelefonoConvencional { get; set; }
        public string Celular { get; set; } = null!;
        public int? IdPatio { get; set; }
        public int Edad { get; set; }

        public virtual Patio? IdPatioNavigation { get; set; }
        public virtual ICollection<SolicitudCredito> SolicitudCreditos { get; set; }
    }
}
