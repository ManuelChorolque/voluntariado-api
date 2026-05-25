using APLICACION.DTOs.Actividadades;
using APLICACION.CasosUso.Actividades;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly CrearActividadHandler _crearHandler;
        private readonly ObtenerActividadesHandler _obtenerTodosHandler;
        private readonly ObtenerActividadPorIdHandler _obtenerPorIdHandler;
        private readonly ObtenerActividadesAbiertasHandler _obtenerAbiertasHandler;
        private readonly AsignarVoluntarioHandler _asignarVoluntarioHandler;
        private readonly EliminarActividadHandler _eliminarHandler;
        private readonly ObtenerActividadesPorVoluntarioHandler _obtenerPorVoluntarioHandler;
        private readonly ActualizarActividadHandler _actualizarHandler;
        private readonly IniciarActividadHandler _iniciarHandler;
        private readonly CancelarActividadHandler _cancelarHandler;
        private readonly CompletarActividadHandler _completarHandler;
        private readonly CurrentUser _currentUser;
        private readonly ILogger<ActividadesController> _logger;

        public ActividadesController(
            CrearActividadHandler crearHandler,
            ObtenerActividadesHandler obtenerTodosHandler,
            ObtenerActividadPorIdHandler obtenerPorIdHandler,
            ObtenerActividadesAbiertasHandler obtenerAbiertasHandler,
            AsignarVoluntarioHandler asignarVoluntarioHandler,
            EliminarActividadHandler eliminarHandler,
            ObtenerActividadesPorVoluntarioHandler obtenerPorVoluntarioHandler,
            ActualizarActividadHandler actualizarHandler,
            IniciarActividadHandler iniciarHandler,
            CancelarActividadHandler cancelarHandler,
            CompletarActividadHandler completarHandler,
            CurrentUser currentUser,
            ILogger<ActividadesController> logger)
        {
            _crearHandler = crearHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _obtenerAbiertasHandler = obtenerAbiertasHandler;
            _asignarVoluntarioHandler = asignarVoluntarioHandler;
            _eliminarHandler = eliminarHandler;
            _obtenerPorVoluntarioHandler = obtenerPorVoluntarioHandler;
            _actualizarHandler = actualizarHandler;
            _iniciarHandler = iniciarHandler;
            _cancelarHandler = cancelarHandler;
            _completarHandler = completarHandler;
            _currentUser = currentUser;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerTodas(
            int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null,
            DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                var orgId = _currentUser.EsOrganizacion ? _currentUser.OrganizacionId : null;
                var actividades = await _obtenerTodosHandler.Ejecutar(pageNumber, pageSize, orgId, busquedaNombre, fechaDesde, fechaHasta);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActividadRespuestaDTO>> ObtenerPorId(int id)
        {
            try
            {
                var actividad = await _obtenerPorIdHandler.Ejecutar(id);
                if (actividad == null)
                    return NotFound();

                return Ok(actividad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividad por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<ActividadRespuestaDTO>> Crear([FromBody] CrearActividadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_currentUser.EsOrganizacion)
                    dto.OrganizacionId = _currentUser.OrganizacionId!.Value;

                var actividad = await _crearHandler.Ejecutar(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = actividad.Id }, actividad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _eliminarHandler.Ejecutar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("organizacion/{organizacionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                var actividades = await _obtenerTodosHandler.Ejecutar(organizacionId: organizacionId);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades por organización");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("abiertas")]
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerActividadesAbiertas()
        {
            try
            {
                var excluirVoluntarioId = _currentUser.EsVoluntario ? _currentUser.VoluntarioId : null;
                var actividades = await _obtenerAbiertasHandler.Ejecutar(excluirVoluntarioId);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades abiertas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("voluntario/{voluntarioId}")]
        [Authorize(Roles = "Admin,Organizacion,Voluntario")]
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerPorVoluntario(int voluntarioId)
        {
            try
            {
                if (_currentUser.EsVoluntario && voluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                var actividades = await _obtenerPorVoluntarioHandler.Ejecutar(voluntarioId);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades por voluntario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<ActividadRespuestaDTO>> Actualizar(int id, [FromBody] ActualizarActividadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var actividad = await _actualizarHandler.Ejecutar(id, dto);
                return Ok(actividad);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/iniciar")]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<IActionResult> Iniciar(int id)
        {
            try
            {
                await _iniciarHandler.Ejecutar(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al iniciar actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/cancelar")]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<IActionResult> Cancelar(int id)
        {
            try
            {
                await _cancelarHandler.Ejecutar(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cancelar actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/completar")]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<IActionResult> Completar(int id)
        {
            try
            {
                await _completarHandler.Ejecutar(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al completar actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/voluntario/{voluntarioId}")]
        [Authorize(Roles = "Admin,Organizacion,Voluntario")]
        public async Task<IActionResult> AsignarVoluntario(int id, int voluntarioId)
        {
            try
            {
                if (_currentUser.EsVoluntario && voluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                await _asignarVoluntarioHandler.Ejecutar(id, voluntarioId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar voluntario a actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
