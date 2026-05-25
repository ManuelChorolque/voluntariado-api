using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Actividadades;

namespace APLICACION.CasosUso.Actividades
{
    public class ActualizarActividadHandler
    {
        private readonly IRepositorioActividades _repositorioActividades;
        private readonly IMapper _mapper;

        public ActualizarActividadHandler(IRepositorioActividades repositorioActividades, IMapper mapper)
        {
            _repositorioActividades = repositorioActividades;
            _mapper = mapper;
        }

        public async Task<ActividadRespuestaDTO> Ejecutar(int id, ActualizarActividadDTO dto)
        {
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la actividad con ID {id}");

            actividad.Nombre = dto.Nombre.Trim();
            actividad.Descripcion = dto.Descripcion.Trim();
            actividad.FechaInicio = dto.FechaInicio;
            actividad.FechaFin = dto.FechaFin;
            actividad.Ubicacion = dto.Ubicacion.Trim();
            actividad.VoluntariosRequeridos = dto.VoluntariosRequeridos;

            await _repositorioActividades.GuardarCambiosAsync();

            return _mapper.Map<ActividadRespuestaDTO>(actividad);
        }
    }
}
