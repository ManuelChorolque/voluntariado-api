using APLICACION.DTOs.Voluntarios;
using APLICACION.CasosUso.Voluntarios;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoluntariosController : ControllerBase
    {
        private readonly CrearVoluntarioHandler _crearHandler;
        private readonly ObtenerVoluntariosHandler _obtenerTodosHandler;
        private readonly ObtenerVoluntarioPorIdHandler _obtenerPorIdHandler;
        private readonly ActualizarVoluntarioHandler _actualizarHandler;
        private readonly EliminarVoluntarioHandler _eliminarHandler;
        private readonly ILogger<VoluntariosController> _logger;

        public VoluntariosController(
            CrearVoluntarioHandler crearHandler,
            ObtenerVoluntariosHandler obtenerTodosHandler,
            ObtenerVoluntarioPorIdHandler obtenerPorIdHandler,
            ActualizarVoluntarioHandler actualizarHandler,
            EliminarVoluntarioHandler eliminarHandler,
            ILogger<VoluntariosController> logger)
        {
            _crearHandler = crearHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _actualizarHandler = actualizarHandler;
            _eliminarHandler = eliminarHandler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoluntarioRespuestaDTO>>> ObtenerTodos()
        {
            try
            {
                var voluntarios = await _obtenerTodosHandler.Ejecutar();
                return Ok(voluntarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener voluntarios");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoluntarioRespuestaDTO>> ObtenerPorId(int id)
        {
            try
            {
                var voluntario = await _obtenerPorIdHandler.Ejecutar(id);
                if (voluntario == null)
                    return NotFound();

                return Ok(voluntario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener voluntario por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VoluntarioRespuestaDTO>> Crear([FromBody] CrearVoluntarioDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var voluntario = await _crearHandler.Ejecutar(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = voluntario.Id }, voluntario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear voluntario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VoluntarioRespuestaDTO>> Actualizar(int id, [FromBody] ActualizarVoluntarioDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("El ID no coincide");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var voluntario = await _actualizarHandler.Ejecutar(id, dto);
                return Ok(voluntario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar voluntario");
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
                _logger.LogError(ex, "Error al eliminar voluntario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("organizacion/{organizacionId}")]
        public async Task<ActionResult<IEnumerable<VoluntarioRespuestaDTO>>> ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                var voluntarios = await _obtenerTodosHandler.Ejecutar(organizacionId: organizacionId);
                return Ok(voluntarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener voluntarios por organización");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
