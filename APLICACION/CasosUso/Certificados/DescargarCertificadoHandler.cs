using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Certificados
{
    public class DescargarCertificadoHandler
    {
        private readonly IRepositorioCertificados _repositorioCertificados;
        private readonly IRepositorioVoluntarios _repositorioVoluntarios;
        private readonly IGeneradorPdf _generadorPdf;

        public DescargarCertificadoHandler(
            IRepositorioCertificados repositorioCertificados,
            IRepositorioVoluntarios repositorioVoluntarios,
            IGeneradorPdf generadorPdf)
        {
            _repositorioCertificados = repositorioCertificados;
            _repositorioVoluntarios = repositorioVoluntarios;
            _generadorPdf = generadorPdf;
        }

        public async Task<byte[]> Ejecutar(int certificadoId)
        {
            var certificado = await _repositorioCertificados.ObtenerPorIdAsync(certificadoId);
            if (certificado == null)
                throw new KeyNotFoundException($"No se encontr\u00f3 el certificado con ID {certificadoId}");

            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(certificado.VoluntarioId);
            if (voluntario == null)
                throw new KeyNotFoundException($"No se encontr\u00f3 el voluntario asociado al certificado");

            return _generadorPdf.GenerarCertificadoPdf(
                nombreVoluntario: voluntario.Nombre,
                horasTotales: (double)voluntario.HorasTotales,
                tema: certificado.TemaEspecifico,
                numeroSerie: certificado.NumeroSerie,
                fechaGeneracion: certificado.FechaEmision,
                firmanteNombre: certificado.FirmanteNombre,
                firmanteCargo: certificado.FirmanteCargo
            );
        }
    }
}
