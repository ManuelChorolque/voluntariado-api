using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using DOMINIO.Enumerados;
using APLICACION.DTOs.Actividadades;
using APLICACION.Utilidades;


namespace APLICACION.CasosUso.Actividades
{
    public class CrearActividadHandler
    {
        private readonly IRepositorioActividades _repositorioActividades;
        private readonly IRepositorioOrganizaciones _repositorioOrganizaciones;
        private readonly IMapper _mapper;

        public CrearActividadHandler(
            IRepositorioActividades repositorioActividades,
            IRepositorioOrganizaciones repositorioOrganizaciones,
            IMapper mapper)
        {
            _repositorioActividades = repositorioActividades;
            _repositorioOrganizaciones = repositorioOrganizaciones;
            _mapper = mapper;
        }

        public async Task<ActividadRespuestaDTO> Ejecutar(CrearActividadDTO dto)
        {
            // Validar nombre
            ValidadorCampos.ValidarCampoRequerido(dto.Nombre, nameof(dto.Nombre));

            // Validar que la organización existe
            var organizacion = await _repositorioOrganizaciones.ObtenerPorIdAsync(dto.OrganizacionId);
            if (organizacion == null)
            {
                throw new KeyNotFoundException($"No se encontró la organización con ID {dto.OrganizacionId}");
            }

            // Validar fechas
            if (dto.FechaFin <= dto.FechaInicio)
            {
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio");
            }

            // Crear entidad
            var actividad = new Actividad
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                OrganizacionId = dto.OrganizacionId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                VoluntariosRequeridos = dto.VoluntariosRequeridos,
                Ubicacion = dto.Ubicacion,
                FechaRegistro = DateTime.UtcNow,
                Estado = EstadoActividad.Planificada
            };

            // Guardar
            await _repositorioActividades.AgregarAsync(actividad);
            await _repositorioActividades.GuardarCambiosAsync();

            // Retornar DTO
            return _mapper.Map<ActividadRespuestaDTO>(actividad);
        }
    }
}
