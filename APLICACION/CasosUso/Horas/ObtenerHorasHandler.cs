using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Horas;

namespace APLICACION.CasosUso.Horas
{
    public class ObtenerHorasHandler
    {
        private readonly IRepositorioHoras _repositorio;
        private readonly IMapper _mapper;

        public ObtenerHorasHandler(IRepositorioHoras repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<HorasRespuestaDTO>> Ejecutar(int pageNumber = 1, int pageSize = 10, int? voluntarioId = null, int? actividadId = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            // Obtener horas con filtros opcionales
            var horas = await _repositorio.ObtenerTodosAsync(pageNumber, pageSize, voluntarioId, actividadId, fechaInicio, fechaFin);
            return _mapper.Map<List<HorasRespuestaDTO>>(horas);
        }
    }
}
