using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Organizaciones
{
    public class CrearOrganizacionDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SitioWeb { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }
}
