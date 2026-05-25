using APLICACION.DTOs.Horas;
using APLICACION.CasosUso.Horas;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HorasVoluntariadoController : ControllerBase
    {
        private readonly RegistrarHorasHandler _registrarHandler;
        private readonly ObtenerHorasHandler _obtenerTodosHandler;
        private readonly ObtenerHoraPorIdHandler _obtenerPorIdHandler;
        private readonly CalcularHorasTotalesHandler _calcularTotalesHandler;
        private readonly EliminarHorasHandler _eliminarHandler;
        private readonly CurrentUser _currentUser;
        private readonly ILogger<HorasVoluntariadoController> _logger;

        public HorasVoluntariadoController(
            RegistrarHorasHandler registrarHandler,
            ObtenerHorasHandler obtenerTodosHandler,
            ObtenerHoraPorIdHandler obtenerPorIdHandler,
            CalcularHorasTotalesHandler calcularTotalesHandler,
            EliminarHorasHandler eliminarHandler,
            CurrentUser currentUser,
            ILogger<HorasVoluntariadoController> logger)
        {
            _registrarHandler = registrarHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _calcularTotalesHandler = calcularTotalesHandler;
            _eliminarHandler = eliminarHandler;
            _currentUser = currentUser;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<IEnumerable<HorasRespuestaDTO>>> ObtenerTodos()
        {
            try
            {
                var horas = await _obtenerTodosHandler.Ejecutar();
                return Ok(horas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorasRespuestaDTO>> ObtenerPorId(int id)
        {
            try
            {
                var horas = await _obtenerPorIdHandler.Ejecutar(id);
                if (horas == null)
                    return NotFound();

                if (_currentUser.EsVoluntario && horas.VoluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                return Ok(horas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horas por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<HorasRespuestaDTO>> Registrar([FromBody] RegistrarHorasDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var horas = await _registrarHandler.Ejecutar(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = horas.Id }, horas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar horas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _eliminarHandler.Ejecutar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar registro de horas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("voluntario/{voluntarioId}")]
        public async Task<ActionResult<IEnumerable<HorasRespuestaDTO>>> ObtenerPorVoluntario(int voluntarioId)
        {
            try
            {
                if (_currentUser.EsVoluntario && voluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                var horas = await _obtenerTodosHandler.Ejecutar(voluntarioId: voluntarioId);
                return Ok(horas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horas por voluntario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("actividad/{actividadId}")]
        public async Task<ActionResult<IEnumerable<HorasRespuestaDTO>>> ObtenerPorActividad(int actividadId)
        {
            try
            {
                var horas = await _obtenerTodosHandler.Ejecutar(actividadId: actividadId);
                return Ok(horas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horas por actividad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("total/{voluntarioId}")]
        public async Task<ActionResult<double>> ObtenerTotalHoras(int voluntarioId)
        {
            try
            {
                if (_currentUser.EsVoluntario && voluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                var total = await _calcularTotalesHandler.Ejecutar(voluntarioId);
                return Ok(total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener total de horas");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
