using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnboardingTest.Entity.Models;

namespace OnboardingTest.Repository.Context
{
    public partial class CreditoAutomotrizContext : DbContext
    {
        public CreditoAutomotrizContext()
        {
        }

        public CreditoAutomotrizContext(DbContextOptions<CreditoAutomotrizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClientePatio> ClientePatios { get; set; } = null!;
        public virtual DbSet<Ejecutivo> Ejecutivos { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Patio> Patios { get; set; } = null!;
        public virtual DbSet<SolicitudCredito> SolicitudCreditos { get; set; } = null!;
        public virtual DbSet<TrackingSolicitud> TrackingSolicituds { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=CreditoAutomotriz;Username=postgres;Password=sa.1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.Id, "cliente_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('cliente_id_seq'::regclass)");

                entity.Property(e => e.Apellidos).HasMaxLength(200);

                entity.Property(e => e.Direccion).HasMaxLength(150);

                entity.Property(e => e.EstadoCivil).HasMaxLength(50);

                entity.Property(e => e.Identificacion).HasMaxLength(10);

                entity.Property(e => e.IdentificacionConyugue).HasMaxLength(10);

                entity.Property(e => e.NombreConyugue).HasMaxLength(200);

                entity.Property(e => e.Nombres).HasMaxLength(100);

                entity.Property(e => e.SujetoCredito).HasDefaultValueSql("false");

                entity.Property(e => e.Telefono).HasMaxLength(9);
            });

            modelBuilder.Entity<ClientePatio>(entity =>
            {
                entity.ToTable("ClientePatio");

                entity.HasIndex(e => e.IdCliente, "clienteclientepatio_fk");

                entity.HasIndex(e => e.Id, "clientepatio_pk")
                    .IsUnique();

                entity.HasIndex(e => e.IdPatio, "patioclientepatio_fk");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('clientepatio_id_seq'::regclass)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClientePatios)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ClienteP_ClienteCl_Cliente");

                entity.HasOne(d => d.IdPatioNavigation)
                    .WithMany(p => p.ClientePatios)
                    .HasForeignKey(d => d.IdPatio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ClienteP_PatioClie_Patio");
            });

            modelBuilder.Entity<Ejecutivo>(entity =>
            {
                entity.ToTable("Ejecutivo");

                entity.HasIndex(e => e.Id, "ejecutivo_pk")
                    .IsUnique();

                entity.HasIndex(e => e.IdPatio, "patioejecutivo_fk");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('ejecutivo_id_seq'::regclass)");

                entity.Property(e => e.Apellidos).HasMaxLength(200);

                entity.Property(e => e.Celular).HasMaxLength(10);

                entity.Property(e => e.Direccion).HasMaxLength(150);

                entity.Property(e => e.Identificacion).HasMaxLength(10);

                entity.Property(e => e.Nombres).HasMaxLength(100);

                entity.Property(e => e.TelefonoConvencional).HasMaxLength(9);

                entity.HasOne(d => d.IdPatioNavigation)
                    .WithMany(p => p.Ejecutivos)
                    .HasForeignKey(d => d.IdPatio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Ejecutiv_PatioEjec_Patio");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("Marca");

                entity.HasIndex(e => e.Id, "marca_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('marca_id_seq'::regclass)");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Patio>(entity =>
            {
                entity.ToTable("Patio");

                entity.HasIndex(e => e.Id, "patio_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('patio_id_seq'::regclass)");

                entity.Property(e => e.Direccion).HasMaxLength(200);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(9);
            });

            modelBuilder.Entity<SolicitudCredito>(entity =>
            {
                entity.ToTable("SolicitudCredito");

                entity.HasIndex(e => e.IdCliente, "clientesolicitudcredito_fk");

                entity.HasIndex(e => e.IdEjecutivo, "ejecutivosolicitudcredito_fk");

                entity.HasIndex(e => e.IdPatio, "patiosolicitudcredito_fk");

                entity.HasIndex(e => e.Id, "solicitudcredito_pk")
                    .IsUnique();

                entity.HasIndex(e => e.IdVehiculo, "vehiculosolicitudcredito_fk");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('solicitudcredito_id_seq'::regclass)");

                entity.Property(e => e.Cuotas).HasColumnType("money");

                entity.Property(e => e.Entrada).HasColumnType("money");

                entity.Property(e => e.Observaciones).HasMaxLength(255);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.SolicitudCreditos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Solicitu_ClienteSo_Cliente");

                entity.HasOne(d => d.IdEjecutivoNavigation)
                    .WithMany(p => p.SolicitudCreditos)
                    .HasForeignKey(d => d.IdEjecutivo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Solicitu_Ejecutivo_Ejecutiv");

                entity.HasOne(d => d.IdPatioNavigation)
                    .WithMany(p => p.SolicitudCreditos)
                    .HasForeignKey(d => d.IdPatio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Solicitu_PatioSoli_Patio");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.SolicitudCreditos)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Solicitu_VehiculoS_Vehiculo");
            });

            modelBuilder.Entity<TrackingSolicitud>(entity =>
            {
                entity.ToTable("TrackingSolicitud");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.HasOne(d => d.IdSolicitudNavigation)
                    .WithMany(p => p.TrackingSolicituds)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_TrackingSolicitud");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");

                entity.HasIndex(e => e.IdMarca, "marcavehiculo_fk");

                entity.HasIndex(e => e.Id, "vehiculo_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('vehiculo_id_seq'::regclass)");

                entity.Property(e => e.Avaluo).HasColumnType("money");

                entity.Property(e => e.Cilindraje).HasPrecision(10, 2);

                entity.Property(e => e.Modelo).HasMaxLength(50);

                entity.Property(e => e.Placa).HasMaxLength(25);

                entity.Property(e => e.Tipo).HasMaxLength(25);

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Vehiculo_MarcaVehi_Marca");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
