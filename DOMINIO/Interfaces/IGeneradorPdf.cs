namespace DOMINIO.Interfaces
{
    public interface IGeneradorPdf
    {
        byte[] GenerarCertificadoPdf(
            string nombreVoluntario,
            double horasTotales,
            string tema,
            string numeroSerie,
            DateTime fechaGeneracion,
            string firmanteNombre,
            string firmanteCargo);
    }
}
