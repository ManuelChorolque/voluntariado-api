using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Certificados
{
    public class EliminarCertificadoHandler
    {
        private readonly IRepositorioCertificados _repositorio;

        public EliminarCertificadoHandler(IRepositorioCertificados repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(int id)
        {
            var certificado = await _repositorio.ObtenerPorIdAsync(id);
            if (certificado == null)
                throw new KeyNotFoundException($"No se encontró el certificado con ID {id}");

            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}
