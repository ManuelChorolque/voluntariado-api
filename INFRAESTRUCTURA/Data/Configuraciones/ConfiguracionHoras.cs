using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DOMINIO.Entidades;

namespace INFRAESTRUCTURA.Data.Configuraciones
{
    public class ConfiguracionHoras : IEntityTypeConfiguration<HorasVoluntariado>
    {
        public void Configure(EntityTypeBuilder<HorasVoluntariado> builder)
        {
            builder.ToTable("HorasVoluntariado");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.VoluntarioId)
                .IsRequired();

            builder.Property(h => h.ActividadId)
                .IsRequired();

            builder.Property(h => h.Horas)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(h => h.FechaRegistro)
                .IsRequired();

            builder.Property(h => h.Descripcion)
                .HasMaxLength(500);

            builder.Property(h => h.FechaActividad)
                .IsRequired();

            builder.HasOne(h => h.Voluntario)
                .WithMany(v => v.HorasRegistradas)
                .HasForeignKey(h => h.VoluntarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Actividad)
                .WithMany(a => a.HorasRegistradas)
                .HasForeignKey(h => h.ActividadId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(h => h.VoluntarioId);
            builder.HasIndex(h => h.ActividadId);
            builder.HasIndex(h => h.FechaActividad);
        }
    }
}
