using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DOMINIO.Entidades;

namespace INFRAESTRUCTURA.Data.Configuraciones
{
    public class ConfiguracionVoluntario : IEntityTypeConfiguration<Voluntario>
    {
        public void Configure(EntityTypeBuilder<Voluntario> builder)
        {
            builder.ToTable("Voluntarios");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(v => v.Cedula)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(v => v.FechaRegistro)
                .IsRequired();

            builder.Property(v => v.HorasTotales)
                .HasPrecision(10, 2);

            builder.Property(v => v.Estado)
                .HasConversion<int>();

            builder.HasOne(v => v.Organizacion)
                .WithMany(o => o.Voluntarios)
                .HasForeignKey(v => v.OrganizacionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(v => v.HorasRegistradas)
                .WithOne(h => h.Voluntario)
                .HasForeignKey(h => h.VoluntarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Certificados)
                .WithOne(c => c.Voluntario)
                .HasForeignKey(c => c.VoluntarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.ActividadesAsignadas)
                .WithMany(a => a.VoluntariosAsignados)
                .UsingEntity("ActividadVoluntario",
                    j => j.HasOne(typeof(Actividad)).WithMany().HasForeignKey("ActividadId"),
                    j => j.HasOne(typeof(Voluntario)).WithMany().HasForeignKey("VoluntarioId"));

            // Índices
            builder.HasIndex(v => v.Cedula).IsUnique();
            builder.HasIndex(v => v.Email);
            builder.HasIndex(v => v.OrganizacionId);
        }
    }
}
