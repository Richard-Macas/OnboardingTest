using System;
using System.Collections.Generic;

namespace OnboardingTest.Entity.Models
{
    public partial class Patio
    {
        public Patio()
        {
            ClientePatios = new HashSet<ClientePatio>();
            Ejecutivos = new HashSet<Ejecutivo>();
            SolicitudCreditos = new HashSet<SolicitudCredito>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int NumeroPuntoVenta { get; set; }

        public virtual ICollection<ClientePatio> ClientePatios { get; set; }
        public virtual ICollection<Ejecutivo> Ejecutivos { get; set; }
        public virtual ICollection<SolicitudCredito> SolicitudCreditos { get; set; }
    }
}
