namespace DOMINIO.Entidades
{
    public class HorasVoluntariado
    {
        public int Id { get; set; }
        public int VoluntarioId { get; set; }
        public int ActividadId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Horas { get; set; }
        public DateTime FechaActividad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        // Relaciones
        public virtual Voluntario Voluntario { get; set; } = null!;
        public virtual Actividad Actividad { get; set; } = null!;
    }
}
