using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Certificados;

namespace APLICACION.CasosUso.Certificados
{
    public class ObtenerCertificadosPorVoluntarioHandler
    {
        private readonly IRepositorioCertificados _repositorio;
        private readonly IMapper _mapper;

        public ObtenerCertificadosPorVoluntarioHandler(IRepositorioCertificados repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<CertificadoRespuestaDTO>> Ejecutar(int voluntarioId)
        {
            var certificados = await _repositorio.ObtenerPorVoluntarioAsync(voluntarioId);
            return _mapper.Map<List<CertificadoRespuestaDTO>>(certificados);
        }
    }
}
