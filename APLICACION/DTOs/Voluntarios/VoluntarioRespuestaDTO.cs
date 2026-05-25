namespace APLICACION.DTOs.Voluntarios
{
    public class VoluntarioRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal HorasTotales { get; set; }
        public int? OrganizacionId { get; set; }
        public string? NombreOrganizacion { get; set; }
    }
}
