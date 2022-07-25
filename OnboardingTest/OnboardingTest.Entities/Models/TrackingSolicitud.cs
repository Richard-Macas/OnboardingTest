using System;
using System.Collections.Generic;

namespace OnboardingTest.Entity.Models
{
    public partial class TrackingSolicitud
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public DateOnly FechaActualizacion { get; set; }
        public string Estado { get; set; } = null!;

        public virtual SolicitudCredito IdSolicitudNavigation { get; set; } = null!;

    }
}
