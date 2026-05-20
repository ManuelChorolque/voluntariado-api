using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DOMINIO.Entidades;

namespace INFRAESTRUCTURA.Data.Configuraciones
{
    public class ConfiguracionCertificado : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificados");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.VoluntarioId)
                .IsRequired();

            builder.Property(c => c.OrganizacionId)
                .IsRequired();

            builder.Property(c => c.NumeroSerie)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(c => c.FechaEmision)
                .IsRequired();

            builder.Property(c => c.HorasTotales)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(c => c.FechaVencimiento)
                .IsRequired();

            builder.Property(c => c.Estado)
                .HasConversion<int>();

            builder.Property(c => c.UrlArchivo)
                .HasMaxLength(500);

            builder.HasOne(c => c.Voluntario)
                .WithMany(v => v.Certificados)
                .HasForeignKey(c => c.VoluntarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Organizacion)
                .WithMany(o => o.Certificados)
                .HasForeignKey(c => c.OrganizacionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(c => c.NumeroSerie).IsUnique();
            builder.HasIndex(c => c.VoluntarioId);
            builder.HasIndex(c => c.OrganizacionId);
        }
    }
}
