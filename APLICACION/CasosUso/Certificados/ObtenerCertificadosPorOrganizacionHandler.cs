using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Certificados;

namespace APLICACION.CasosUso.Certificados
{
    public class ObtenerCertificadosPorOrganizacionHandler
    {
        private readonly IRepositorioCertificados _repositorio;
        private readonly IMapper _mapper;

        public ObtenerCertificadosPorOrganizacionHandler(IRepositorioCertificados repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<CertificadoRespuestaDTO>> Ejecutar(int organizacionId)
        {
            var certificados = await _repositorio.ObtenerPorOrganizacionAsync(organizacionId);
            return _mapper.Map<List<CertificadoRespuestaDTO>>(certificados);
        }
    }
}
