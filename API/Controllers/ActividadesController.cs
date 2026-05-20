using APLICACION.DTOs.Actividadades;
using APLICACION.CasosUso.Actividades;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        private readonly ILogger<ActividadesController> _logger;

        public ActividadesController(
            CrearActividadHandler crearHandler,
            ObtenerActividadesHandler obtenerTodosHandler,
            ObtenerActividadPorIdHandler obtenerPorIdHandler,
            ObtenerActividadesAbiertasHandler obtenerAbiertasHandler,
            AsignarVoluntarioHandler asignarVoluntarioHandler,
            EliminarActividadHandler eliminarHandler,
            ILogger<ActividadesController> logger)
        {
            _crearHandler = crearHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _obtenerAbiertasHandler = obtenerAbiertasHandler;
            _asignarVoluntarioHandler = asignarVoluntarioHandler;
            _eliminarHandler = eliminarHandler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerTodas()
        {
            try
            {
                var actividades = await _obtenerTodosHandler.Ejecutar();
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
        public async Task<ActionResult<ActividadRespuestaDTO>> Crear([FromBody] CrearActividadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

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
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                var actividades = await _obtenerTodosHandler.Ejecutar(organizacionId: organizacionId);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades por organizaci\u00f3n");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("abiertas")]
        public async Task<ActionResult<IEnumerable<ActividadRespuestaDTO>>> ObtenerActividadesAbiertas()
        {
            try
            {
                var actividades = await _obtenerAbiertasHandler.Ejecutar();
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades abiertas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/voluntario/{voluntarioId}")]
        public async Task<IActionResult> AsignarVoluntario(int id, int voluntarioId)
        {
            try
            {
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
