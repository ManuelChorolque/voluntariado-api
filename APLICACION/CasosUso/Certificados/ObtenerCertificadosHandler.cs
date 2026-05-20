using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Certificados;

namespace APLICACION.CasosUso.Certificados
{
    public class ObtenerCertificadosHandler
    {
        private readonly IRepositorioCertificados _repositorio;
        private readonly IMapper _mapper;

        public ObtenerCertificadosHandler(IRepositorioCertificados repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<CertificadoRespuestaDTO>> Ejecutar(int pageNumber = 1, int pageSize = 10)
        {
            var certificados = await _repositorio.ObtenerTodosAsync(pageNumber, pageSize);
            return _mapper.Map<List<CertificadoRespuestaDTO>>(certificados);
        }
    }
}
