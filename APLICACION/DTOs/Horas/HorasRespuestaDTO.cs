using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Horas
{
    public class HorasRespuestaDTO
    {
        public int Id { get; set; }
        public int VoluntarioId { get; set; }
        public string NombreVoluntario { get; set; } = string.Empty;
        public int ActividadId { get; set; }
        public string NombreActividad { get; set; } = string.Empty;
        public decimal Horas { get; set; }
        public DateTime FechaActividad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
