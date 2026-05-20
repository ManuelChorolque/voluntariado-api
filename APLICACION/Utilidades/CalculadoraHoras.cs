using APLICACION.Utilidades;

namespace APLICACION.Utilidades
{
    public class CalculadoraHoras
    {
        public static decimal CalcularTotalHoras(IEnumerable<decimal> horas)
        {
            return horas.Sum();
        }

        public static decimal CalcularPromedioHoras(IEnumerable<decimal> horas)
        {
            var lista = horas.ToList();
            if (lista.Count == 0)
                return 0;

            return lista.Sum() / lista.Count;
        }

        public static TimeSpan CalcularDiferenciaFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return fechaFin - fechaInicio;
        }

        public static int CalcularDiasTranscurridos(DateTime fecha)
        {
            return (int)(DateTime.UtcNow - fecha).TotalDays;
        }

        public static bool EsHoraValida(decimal horas)
        {
            return horas > 0 && horas <= 24;
        }

        public static decimal RedondearHoras(decimal horas, int decimales = 2)
        {
            return Math.Round(horas, decimales);
        }
    }
}
