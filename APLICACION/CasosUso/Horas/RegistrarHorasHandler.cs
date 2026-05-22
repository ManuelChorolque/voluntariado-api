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
            var voluntario = await ValidarHorasAsync(dto);
            var horasDecimales = CalcularHoras(dto);
            var horasVoluntariado = await GuardarRegistroAsync(dto, voluntario, horasDecimales);
            await EnviarNotificacionAsync(voluntario, horasDecimales);

            return _mapper.Map<HorasRespuestaDTO>(horasVoluntariado);
        }

        private async Task<Voluntario> ValidarHorasAsync(RegistrarHorasDTO dto)
        {
            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(dto.VoluntarioId)
                ?? throw new KeyNotFoundException($"No se encontró el voluntario con ID {dto.VoluntarioId}");

            var actividad = await _repositorioActividades.ObtenerPorIdAsync(dto.ActividadId)
                ?? throw new KeyNotFoundException($"No se encontró la actividad con ID {dto.ActividadId}");

            if (dto.FechaFin <= dto.FechaInicio)
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio");

            if (!CalculadoraHoras.EsHoraValida((decimal)(dto.FechaFin - dto.FechaInicio).TotalHours))
                throw new ArgumentException("Las horas registradas no son válidas (deben ser entre 1 y 24)");

            return voluntario;
        }

        private static decimal CalcularHoras(RegistrarHorasDTO dto)
        {
            return CalculadoraHoras.RedondearHoras(
                (decimal)(dto.FechaFin - dto.FechaInicio).TotalHours);
        }

        private async Task<HorasVoluntariado> GuardarRegistroAsync(RegistrarHorasDTO dto, Voluntario voluntario, decimal horasDecimales)
        {
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

            await _repositorioHoras.AgregarAsync(horasVoluntariado);

            voluntario.HorasTotales += horasDecimales;
            await _repositorioVoluntarios.ActualizarAsync(voluntario);

            await _repositorioHoras.GuardarCambiosAsync();

            return horasVoluntariado;
        }

        private async Task EnviarNotificacionAsync(Voluntario voluntario, decimal horas)
        {
            Console.WriteLine($"[NOTIFICACIÓN] Horas registradas para {voluntario.Nombre} {voluntario.Apellido}: {horas}h");
            await Task.CompletedTask;
        }
    }
}
