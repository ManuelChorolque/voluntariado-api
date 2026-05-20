using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DOMINIO.Entidades;

namespace INFRAESTRUCTURA.Data.Configuraciones
{
    public class ConfiguracionOrganizacion : IEntityTypeConfiguration<Organizacion>
    {
        public void Configure(EntityTypeBuilder<Organizacion> builder)
        {
            builder.ToTable("Organizaciones");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(o => o.Descripcion)
                .HasMaxLength(1000);

            builder.Property(o => o.Contacto)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Direccion)
                .HasMaxLength(250);

            builder.Property(o => o.Telefono)
                .HasMaxLength(20);

            builder.Property(o => o.FechaRegistro)
                .IsRequired();

            builder.HasMany(o => o.Voluntarios)
                .WithOne(v => v.Organizacion)
                .HasForeignKey(v => v.OrganizacionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.Actividades)
                .WithOne(a => a.Organizacion)
                .HasForeignKey(a => a.OrganizacionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Certificados)
                .WithOne(c => c.Organizacion)
                .HasForeignKey(c => c.OrganizacionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(o => o.Nombre).IsUnique();
            builder.HasIndex(o => o.Email);
        }
    }
}
