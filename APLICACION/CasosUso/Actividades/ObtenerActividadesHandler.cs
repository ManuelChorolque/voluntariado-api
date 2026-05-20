using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Actividadades;

namespace APLICACION.CasosUso.Actividades
{
    public class ObtenerActividadesHandler
    {
        private readonly IRepositorioActividades _repositorio;
        private readonly IMapper _mapper;

        public ObtenerActividadesHandler(IRepositorioActividades repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<ActividadRespuestaDTO>> Ejecutar(int pageNumber = 1, int pageSize = 10, int? organizacionId = null, string? busquedaNombre = null)
        {
            // Obtener actividades con paginación
            var actividades = await _repositorio.ObtenerTodosAsync(pageNumber, pageSize, organizacionId, busquedaNombre);
            return _mapper.Map<List<ActividadRespuestaDTO>>(actividades);
        }
    }


}
