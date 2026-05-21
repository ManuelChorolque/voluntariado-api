using DOMINIO.Enumeradores;

namespace DOMINIO.Entidades
{
    public class Voluntario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public decimal HorasTotales { get; set; } = 0;
        public DateTime FechaRegistro { get; set; }
        public EstadoVoluntario Estado { get; set; } = EstadoVoluntario.Activo;
        public int? OrganizacionId { get; set; }

        // Relaciones
        public virtual Organizacion? Organizacion { get; set; }
        public virtual ICollection<HorasVoluntariado> HorasRegistradas { get; set; } = new List<HorasVoluntariado>();
        public virtual ICollection<Actividad> ActividadesAsignadas { get; set; } = new List<Actividad>();
        public virtual ICollection<Certificado> Certificados { get; set; } = new List<Certificado>();
    }
}
