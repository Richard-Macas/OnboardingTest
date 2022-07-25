using System;
using System.Collections.Generic;

namespace OnboardingTest.Entity.Models
{
    public partial class SolicitudCredito
    {
        public SolicitudCredito()
        {
            TrackingSolicituds = new HashSet<TrackingSolicitud>();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdPatio { get; set; }
        public int IdVehiculo { get; set; }
        public DateOnly FechaElaboracion { get; set; }
        public int MesesPlazo { get; set; }
        public decimal Cuotas { get; set; }
        public decimal Entrada { get; set; }
        public int IdEjecutivo { get; set; }
        public string? Observaciones { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; } = null!;
        public virtual Ejecutivo? IdEjecutivoNavigation { get; set; } = null!;
        public virtual Patio? IdPatioNavigation { get; set; } = null!;
        public virtual Vehiculo? IdVehiculoNavigation { get; set; } = null!;
        public virtual ICollection<TrackingSolicitud> TrackingSolicituds { get; set; }

    }
}
