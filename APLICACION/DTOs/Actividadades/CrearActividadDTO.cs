using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Actividadades
{
    public class CrearActividadDTO
    {
        public int OrganizacionId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int VoluntariosRequeridos { get; set; }
    }
}
