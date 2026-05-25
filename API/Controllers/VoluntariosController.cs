using APLICACION.DTOs.Voluntarios;
using APLICACION.CasosUso.Voluntarios;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VoluntariosController : ControllerBase
    {
        private readonly CrearVoluntarioHandler _crearHandler;
        private readonly ObtenerVoluntariosHandler _obtenerTodosHandler;
        private readonly ObtenerVoluntarioPorIdHandler _obtenerPorIdHandler;
        private readonly ActualizarVoluntarioHandler _actualizarHandler;
        private readonly EliminarVoluntarioHandler _eliminarHandler;
        private readonly CurrentUser _currentUser;
        private readonly ILogger<VoluntariosController> _logger;

        public VoluntariosController(
            CrearVoluntarioHandler crearHandler,
            ObtenerVoluntariosHandler obtenerTodosHandler,
            ObtenerVoluntarioPorIdHandler obtenerPorIdHandler,
            ActualizarVoluntarioHandler actualizarHandler,
            EliminarVoluntarioHandler eliminarHandler,
            CurrentUser currentUser,
            ILogger<VoluntariosController> logger)
        {
            _crearHandler = crearHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _actualizarHandler = actualizarHandler;
            _eliminarHandler = eliminarHandler;
            _currentUser = currentUser;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<IEnumerable<VoluntarioRespuestaDTO>>> ObtenerTodos(
            int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null)
        {
            try
            {
                var orgId = _currentUser.EsOrganizacion ? _currentUser.OrganizacionId : null;
                var voluntarios = await _obtenerTodosHandler.Ejecutar(pageNumber, pageSize, busquedaNombre, orgId);
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
                if (_currentUser.EsVoluntario && _currentUser.VoluntarioId != id)
                    return Forbid();

                var voluntario = await _obtenerPorIdHandler.Ejecutar(id);
                if (voluntario == null)
                    return NotFound();

                if (_currentUser.EsOrganizacion && _currentUser.OrganizacionId != voluntario.OrganizacionId)
                    return Forbid();

                return Ok(voluntario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener voluntario por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<VoluntarioRespuestaDTO>> Crear([FromBody] CrearVoluntarioDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_currentUser.EsOrganizacion)
                    dto.OrganizacionId = _currentUser.OrganizacionId;

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
        [Authorize(Roles = "Admin,Organizacion")]
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
                _logger.LogError(ex, "Error al eliminar voluntario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("organizacion/{organizacionId}")]
        [Authorize(Roles = "Admin")]
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
