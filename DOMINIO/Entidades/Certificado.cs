using DOMINIO.Enumeradores;

namespace DOMINIO.Entidades
{
    public class Certificado
    {
        public int Id { get; set; }
        public int VoluntarioId { get; set; }
        public int? ActividadId { get; set; }
        public int OrganizacionId { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal HorasTotales { get; set; }
        public string TemaEspecifico { get; set; } = string.Empty;
        public string FirmanteNombre { get; set; } = string.Empty;
        public string FirmanteCargo { get; set; } = string.Empty;
        public string UrlArchivo { get; set; } = string.Empty;
        public EstadoCertificado Estado { get; set; } = EstadoCertificado.Generado;

        // Relaciones
        public virtual Voluntario Voluntario { get; set; } = null!;
        public virtual Actividad? Actividad { get; set; }
        public virtual Organizacion Organizacion { get; set; } = null!;
    }
}
