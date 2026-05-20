using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Organizaciones;

namespace APLICACION.CasosUso.Organizaciones
{
    public class ObtenerOrganizacionPorIdHandler
    {
        private readonly IRepositorioOrganizaciones _repositorio;
        private readonly IMapper _mapper;

        public ObtenerOrganizacionPorIdHandler(IRepositorioOrganizaciones repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<OrganizacionRespuestaDTO> Ejecutar(int id)
        {
            var organizacion = await _repositorio.ObtenerPorIdAsync(id);
            if (organizacion == null)
                throw new KeyNotFoundException($"No se encontró la organización con ID {id}");

            return _mapper.Map<OrganizacionRespuestaDTO>(organizacion);
        }
    }
}
