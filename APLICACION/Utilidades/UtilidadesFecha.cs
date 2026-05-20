using APLICACION.Utilidades;

namespace APLICACION.Utilidades
{
    public class UtilidadesFecha
    {
        public static string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy");
        }

        public static string FormatearFechaHora(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy HH:mm");
        }

        public static int ObtenerAnio(DateTime fecha)
        {
            return fecha.Year;
        }

        public static int ObtenerMes(DateTime fecha)
        {
            return fecha.Month;
        }

        public static int ObtenerDia(DateTime fecha)
        {
            return fecha.Day;
        }

        public static bool EsFechaFutura(DateTime fecha)
        {
            return fecha > DateTime.UtcNow;
        }

        public static bool EsFechaPasada(DateTime fecha)
        {
            return fecha < DateTime.UtcNow;
        }

        public static DateTime ObtenerComienzoDelDia(DateTime fecha)
        {
            return fecha.Date;
        }

        public static DateTime ObtenerFinalDelDia(DateTime fecha)
        {
            return fecha.Date.AddDays(1).AddTicks(-1);
        }

        public static int CalcularDiasEntre(DateTime fechaInicio, DateTime fechaFin)
        {
            return (int)(fechaFin.Date - fechaInicio.Date).TotalDays;
        }
    }
}
