using System;
using System.Collections.Generic;

namespace OnboardingTest.Entity.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            SolicitudCreditos = new HashSet<SolicitudCredito>();
        }

        public int Id { get; set; }
        public string Placa { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int NroChasis { get; set; }
        public int IdMarca { get; set; }
        public string? Tipo { get; set; }
        public decimal Cilindraje { get; set; }
        public decimal Avaluo { get; set; }
        public int Anio { get; set; }

        public virtual Marca? IdMarcaNavigation { get; set; } = null!;
        public virtual ICollection<SolicitudCredito> SolicitudCreditos { get; set; }
    }
}
