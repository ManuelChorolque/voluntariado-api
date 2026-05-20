using APLICACION.DTOs.Actividadades;

namespace APLICACION.DTOs.Certificados
{
    public class GenerarCertificadoDTO
    {
        public int VoluntarioId { get; set; }
        public int? ActividadId { get; set; }
        public int OrganizacionId { get; set; }
        public string TemaEspecifico { get; set; } = string.Empty;
        public string FirmanteNombre { get; set; } = string.Empty;
        public string FirmanteCargo { get; set; } = string.Empty;
    }

}
