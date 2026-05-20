using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Certificados
{
    public class CertificadoRespuestaDTO
    {
        public int Id { get; set; }
        public int VoluntarioId { get; set; }
        public string NombreVoluntario { get; set; } = string.Empty;
        public int OrganizacionId { get; set; }
        public string NombreOrganizacion { get; set; } = string.Empty;
        public string NumeroSerie { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public decimal HorasTotales { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string UrlArchivo { get; set; } = string.Empty;
    }
}
