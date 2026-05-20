using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Horas;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Horas
{
    public class RegistrarHorasHandler
    {
        private readonly IRepositorioHoras _repositorioHoras;
        private readonly IRepositorioVoluntarios _repositorioVoluntarios;
        private readonly IRepositorioActividades _repositorioActividades;
        private readonly IMapper _mapper;

        public RegistrarHorasHandler(
            IRepositorioHoras repositorioHoras,
            IRepositorioVoluntarios repositorioVoluntarios,
            IRepositorioActividades repositorioActividades,
            IMapper mapper)
        {
            _repositorioHoras = repositorioHoras;
            _repositorioVoluntarios = repositorioVoluntarios;
            _repositorioActividades = repositorioActividades;
            _mapper = mapper;
        }

        public async Task<HorasRespuestaDTO> Ejecutar(RegistrarHorasDTO dto)
        {
            // Validar que el voluntario existe
            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(dto.VoluntarioId);
            if (voluntario == null)
            {
                throw new KeyNotFoundException($"No se encontró el voluntario con ID {dto.VoluntarioId}");
            }

            // Validar que la actividad existe
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(dto.ActividadId);
            if (actividad == null)
            {
                throw new KeyNotFoundException($"No se encontró la actividad con ID {dto.ActividadId}");
            }

            // Validar fechas
            if (dto.FechaFin <= dto.FechaInicio)
            {
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio");
            }

            // Calcular horas
            var horasDecimales = (decimal)(dto.FechaFin - dto.FechaInicio).TotalHours;

            // Crear entidad de horas
            var horasVoluntariado = new HorasVoluntariado
            {
                VoluntarioId = dto.VoluntarioId,
                ActividadId = dto.ActividadId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Horas = horasDecimales,
                FechaActividad = dto.FechaInicio,
                Descripcion = dto.Descripcion,
                FechaRegistro = DateTime.UtcNow
            };

            // Guardar
            await _repositorioHoras.AgregarAsync(horasVoluntariado);

            // Actualizar total de horas del voluntario
            voluntario.HorasTotales += horasDecimales;
            await _repositorioVoluntarios.ActualizarAsync(voluntario);

            await _repositorioHoras.GuardarCambiosAsync();

            // Retornar DTO
            return _mapper.Map<HorasRespuestaDTO>(horasVoluntariado);
        }
    }
}
