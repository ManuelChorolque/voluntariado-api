using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using INFRAESTRUCTURA.Data.Configuraciones;

namespace Infraestructura.Data;


public class VoluntariadoDbContext : DbContext
{
    public VoluntariadoDbContext(DbContextOptions<VoluntariadoDbContext> options) : base(options)
    {
    }

    public DbSet<Voluntario> Voluntarios { get; set; } = null!;
    public DbSet<HorasVoluntariado> HorasVoluntariado { get; set; } = null!;
    public DbSet<Certificado> Certificados { get; set; } = null!;
    public DbSet<Organizacion> Organizaciones { get; set; } = null!;
    public DbSet<Actividad> Actividades { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configuraciones
        modelBuilder.ApplyConfiguration(new ConfiguracionVoluntario());
        modelBuilder.ApplyConfiguration(new ConfiguracionHoras());
        modelBuilder.ApplyConfiguration(new ConfiguracionCertificado());
        modelBuilder.ApplyConfiguration(new ConfiguracionOrganizacion());
        modelBuilder.ApplyConfiguration(new ConfiguracionActividad());
    }
}
