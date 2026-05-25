using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DOMINIO.Entidades;

namespace INFRAESTRUCTURA.Data.Configuraciones
{
    public class ConfiguracionUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Rol)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(u => u.FechaRegistro)
                .IsRequired();

            builder.HasOne(u => u.Voluntario)
                .WithMany()
                .HasForeignKey(u => u.VoluntarioId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Organizacion)
                .WithMany()
                .HasForeignKey(u => u.OrganizacionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
