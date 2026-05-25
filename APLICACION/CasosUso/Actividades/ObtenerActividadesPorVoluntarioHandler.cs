using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Actividadades;

namespace APLICACION.CasosUso.Actividades
{
    public class ObtenerActividadesPorVoluntarioHandler
    {
        private readonly IRepositorioActividades _repositorio;
        private readonly IMapper _mapper;

        public ObtenerActividadesPorVoluntarioHandler(IRepositorioActividades repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<ActividadRespuestaDTO>> Ejecutar(int voluntarioId)
        {
            var actividades = await _repositorio.ObtenerPorVoluntarioAsync(voluntarioId);
            return _mapper.Map<List<ActividadRespuestaDTO>>(actividades);
        }
    }
}
