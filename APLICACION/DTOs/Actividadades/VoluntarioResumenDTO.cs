namespace APLICACION.DTOs.Actividadades
{
    public class VoluntarioResumenDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
    }
}
