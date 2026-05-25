using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Actividadades
{
    public class ActividadRespuestaDTO
    {
        public int Id { get; set; }
        public int OrganizacionId { get; set; }
        public string NombreOrganizacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int VoluntariosRequeridos { get; set; }
        public int VoluntariosAsignados { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public List<VoluntarioResumenDTO> Voluntarios { get; set; } = new();
    }

}
