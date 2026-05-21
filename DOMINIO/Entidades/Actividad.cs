using DOMINIO.Enumeradores;

namespace DOMINIO.Entidades
{
    public class Actividad
    {
        public int Id { get; set; }
        public int OrganizacionId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int VoluntariosRequeridos { get; set; }
        public EstadoActividad Estado { get; set; } = EstadoActividad.Planificada;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual Organizacion Organizacion { get; set; } = null!;
        public virtual ICollection<Voluntario> VoluntariosAsignados { get; set; } = new List<Voluntario>();
        public virtual ICollection<HorasVoluntariado> HorasRegistradas { get; set; } = new List<HorasVoluntariado>();
    }
}
