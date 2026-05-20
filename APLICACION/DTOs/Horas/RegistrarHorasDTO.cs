using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Horas
{
    public class RegistrarHorasDTO
    {
        public int VoluntarioId { get; set; }
        public int ActividadId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
