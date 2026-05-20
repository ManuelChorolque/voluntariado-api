using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Certificados;

namespace APLICACION.CasosUso.Certificados
{
    public class ObtenerCertificadoPorIdHandler
    {
        private readonly IRepositorioCertificados _repositorio;
        private readonly IMapper _mapper;

        public ObtenerCertificadoPorIdHandler(IRepositorioCertificados repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<CertificadoRespuestaDTO?> Ejecutar(int id)
        {
            var certificado = await _repositorio.ObtenerPorIdAsync(id);
            return certificado == null ? null : _mapper.Map<CertificadoRespuestaDTO>(certificado);
        }
    }
}
