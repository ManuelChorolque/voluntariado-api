using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Organizaciones;

namespace APLICACION.CasosUso.Organizaciones
{
    public class ObtenerOrganizacionesHandler
    {
        private readonly IRepositorioOrganizaciones _repositorio;
        private readonly IMapper _mapper;

        public ObtenerOrganizacionesHandler(IRepositorioOrganizaciones repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<OrganizacionRespuestaDTO>> Ejecutar(int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null)
        {
            // Obtener organizaciones con paginación
            var organizaciones = await _repositorio.ObtenerTodosAsync(pageNumber, pageSize, busquedaNombre);
            return _mapper.Map<List<OrganizacionRespuestaDTO>>(organizaciones);
        }
    }


}
