using System.Text;
using DOMINIO.Interfaces;

namespace INFRAESTRUCTURA.ServiciosExternos
{
    public class GeneradorPdf : IGeneradorPdf
    {
        public byte[] GenerarCertificadoPdf(
            string nombreVoluntario,
            double horasTotales,
            string tema,
            string numeroSerie,
            DateTime fechaGeneracion,
            string firmanteNombre,
            string firmanteCargo)
        {
            var contenido = new StringBuilder();
            contenido.AppendLine("=== CERTIFICADO DE VOLUNTARIADO ===");
            contenido.AppendLine($"Voluntario: {nombreVoluntario}");
            contenido.AppendLine($"Tema: {tema}");
            contenido.AppendLine($"Horas Totales: {horasTotales:F2}");
            contenido.AppendLine($"N\u00famero de Serie: {numeroSerie}");
            contenido.AppendLine($"Fecha de Emisi\u00f3n: {fechaGeneracion:dd/MM/yyyy}");
            contenido.AppendLine($"Firmante: {firmanteNombre} - {firmanteCargo}");

            return Encoding.UTF8.GetBytes(contenido.ToString());
        }
    }
}
