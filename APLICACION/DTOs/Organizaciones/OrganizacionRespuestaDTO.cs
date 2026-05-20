using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Organizaciones
{
    public class OrganizacionRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public int TotalVoluntarios { get; set; }
        public int TotalActividades { get; set; }
    }
}
