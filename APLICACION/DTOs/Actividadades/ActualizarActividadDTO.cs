using System.ComponentModel.DataAnnotations;

namespace APLICACION.DTOs.Actividadades
{
    public class ActualizarActividadDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public string Ubicacion { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue)]
        public int VoluntariosRequeridos { get; set; }
    }
}
