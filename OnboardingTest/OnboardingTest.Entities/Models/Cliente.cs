using FluentValidation;
using System;
using System.Collections.Generic;

namespace OnboardingTest.Entity.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClientePatios = new HashSet<ClientePatio>();
            SolicitudCreditos = new HashSet<SolicitudCredito>();
        }

        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Edad { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Direccion { get; set; } = null!;
        public string EstadoCivil { get; set; } = null!;
        public string IdentificacionConyugue { get; set; } = null!;
        public string NombreConyugue { get; set; } = null!;
        public bool? SujetoCredito { get; set; }
        public string Telefono { get; set; } = null!;

        public virtual ICollection<ClientePatio> ClientePatios { get; set; }
        public virtual ICollection<SolicitudCredito> SolicitudCreditos { get; set; }
    }
}
