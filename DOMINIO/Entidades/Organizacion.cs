namespace DOMINIO.Entidades
{
    public class Organizacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SitioWeb { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual ICollection<Voluntario> Voluntarios { get; set; } = new List<Voluntario>();
        public virtual ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();
        public virtual ICollection<Certificado> Certificados { get; set; } = new List<Certificado>();
    }
}
