using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Actividadades;

namespace APLICACION.CasosUso.Actividades
{
    public class ObtenerActividadPorIdHandler
    {
        private readonly IRepositorioActividades _repositorio;
        private readonly IMapper _mapper;

        public ObtenerActividadPorIdHandler(IRepositorioActividades repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<ActividadRespuestaDTO?> Ejecutar(int id)
        {
            var actividad = await _repositorio.ObtenerPorIdAsync(id);
            return actividad == null ? null : _mapper.Map<ActividadRespuestaDTO>(actividad);
        }
    }
}
