using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DOMINIO.Entidades;

namespace INFRAESTRUCTURA.Data.Configuraciones
{
    public class ConfiguracionActividad : IEntityTypeConfiguration<Actividad>
    {
        public void Configure(EntityTypeBuilder<Actividad> builder)
        {
            builder.ToTable("Actividades");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.OrganizacionId)
                .IsRequired();

            builder.Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.Descripcion)
                .HasMaxLength(1000);

            builder.Property(a => a.FechaInicio)
                .IsRequired();

            builder.Property(a => a.FechaFin)
                .IsRequired();

            builder.Property(a => a.Ubicacion)
                .HasMaxLength(250);

            builder.Property(a => a.VoluntariosRequeridos)
                .IsRequired();

            builder.Property(a => a.Estado)
                .HasConversion<int>();

            builder.Property(a => a.FechaRegistro)
                .IsRequired();

            builder.HasOne(a => a.Organizacion)
                .WithMany(o => o.Actividades)
                .HasForeignKey(a => a.OrganizacionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.VoluntariosAsignados)
                .WithMany(v => v.ActividadesAsignadas)
                .UsingEntity("ActividadVoluntario",
                    j => j.HasOne(typeof(Actividad)).WithMany().HasForeignKey("ActividadId"),
                    j => j.HasOne(typeof(Voluntario)).WithMany().HasForeignKey("VoluntarioId"));

            builder.HasMany(a => a.HorasRegistradas)
                .WithOne(h => h.Actividad)
                .HasForeignKey(h => h.ActividadId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(a => a.OrganizacionId);
            builder.HasIndex(a => a.FechaInicio);
            builder.HasIndex(a => a.FechaFin);
        }
    }
}
