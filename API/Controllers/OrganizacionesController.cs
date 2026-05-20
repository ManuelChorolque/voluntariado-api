using APLICACION.DTOs.Organizaciones;
using APLICACION.CasosUso.Organizaciones;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionesController : ControllerBase
    {
        private readonly CrearOrganizacionHandler _crearHandler;
        private readonly ObtenerOrganizacionesHandler _obtenerTodosHandler;
        private readonly ObtenerOrganizacionPorIdHandler _obtenerPorIdHandler;
        private readonly ActualizarOrganizacionHandler _actualizarHandler;
        private readonly EliminarOrganizacionHandler _eliminarHandler;
        private readonly ILogger<OrganizacionesController> _logger;

        public OrganizacionesController(
            CrearOrganizacionHandler crearHandler,
            ObtenerOrganizacionesHandler obtenerTodosHandler,
            ObtenerOrganizacionPorIdHandler obtenerPorIdHandler,
            ActualizarOrganizacionHandler actualizarHandler,
            EliminarOrganizacionHandler eliminarHandler,
            ILogger<OrganizacionesController> logger)
        {
            _crearHandler = crearHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _actualizarHandler = actualizarHandler;
            _eliminarHandler = eliminarHandler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizacionRespuestaDTO>>> ObtenerTodas()
        {
            try
            {
                var organizaciones = await _obtenerTodosHandler.Ejecutar();
                return Ok(organizaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener organizaciones");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizacionRespuestaDTO>> ObtenerPorId(int id)
        {
            try
            {
                var organizacion = await _obtenerPorIdHandler.Ejecutar(id);
                return Ok(organizacion);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener organizaci\u00f3n por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrganizacionRespuestaDTO>> Crear([FromBody] CrearOrganizacionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var organizacion = await _crearHandler.Ejecutar(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = organizacion.Id }, organizacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear organizaci\u00f3n");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrganizacionRespuestaDTO>> Actualizar(int id, [FromBody] CrearOrganizacionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var organizacion = await _actualizarHandler.Ejecutar(id, dto);
                return Ok(organizacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar organizaci\u00f3n");
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
                _logger.LogError(ex, "Error al eliminar organizaci\u00f3n");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
