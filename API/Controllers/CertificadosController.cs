using APLICACION.DTOs.Certificados;
using APLICACION.CasosUso.Certificados;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController : ControllerBase
    {
        private readonly GenerarCertificadoHandler _generarHandler;
        private readonly ObtenerCertificadosHandler _obtenerTodosHandler;
        private readonly ObtenerCertificadoPorIdHandler _obtenerPorIdHandler;
        private readonly ObtenerCertificadosPorVoluntarioHandler _obtenerPorVoluntarioHandler;
        private readonly ObtenerCertificadosPorOrganizacionHandler _obtenerPorOrganizacionHandler;
        private readonly DescargarCertificadoHandler _descargarHandler;
        private readonly EliminarCertificadoHandler _eliminarHandler;
        private readonly CurrentUser _currentUser;
        private readonly ILogger<CertificadosController> _logger;

        public CertificadosController(
            GenerarCertificadoHandler generarHandler,
            ObtenerCertificadosHandler obtenerTodosHandler,
            ObtenerCertificadoPorIdHandler obtenerPorIdHandler,
            ObtenerCertificadosPorVoluntarioHandler obtenerPorVoluntarioHandler,
            ObtenerCertificadosPorOrganizacionHandler obtenerPorOrganizacionHandler,
            DescargarCertificadoHandler descargarHandler,
            EliminarCertificadoHandler eliminarHandler,
            CurrentUser currentUser,
            ILogger<CertificadosController> logger)
        {
            _generarHandler = generarHandler;
            _obtenerTodosHandler = obtenerTodosHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _obtenerPorVoluntarioHandler = obtenerPorVoluntarioHandler;
            _obtenerPorOrganizacionHandler = obtenerPorOrganizacionHandler;
            _descargarHandler = descargarHandler;
            _eliminarHandler = eliminarHandler;
            _currentUser = currentUser;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<IEnumerable<CertificadoRespuestaDTO>>> ObtenerTodos()
        {
            try
            {
                var certificados = await _obtenerTodosHandler.Ejecutar();
                return Ok(certificados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener certificados");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CertificadoRespuestaDTO>> ObtenerPorId(int id)
        {
            try
            {
                var certificado = await _obtenerPorIdHandler.Ejecutar(id);
                if (certificado == null)
                    return NotFound();

                if (_currentUser.EsVoluntario && certificado.VoluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                return Ok(certificado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener certificado por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizacion")]
        public async Task<ActionResult<CertificadoRespuestaDTO>> Generar([FromBody] GenerarCertificadoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var certificado = await _generarHandler.Ejecutar(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = certificado.Id }, certificado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar certificado");
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
                _logger.LogError(ex, "Error al eliminar certificado");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("voluntario/{voluntarioId}")]
        public async Task<ActionResult<IEnumerable<CertificadoRespuestaDTO>>> ObtenerPorVoluntario(int voluntarioId)
        {
            try
            {
                if (_currentUser.EsVoluntario && voluntarioId != _currentUser.VoluntarioId)
                    return Forbid();

                var certificados = await _obtenerPorVoluntarioHandler.Ejecutar(voluntarioId);
                return Ok(certificados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener certificados por voluntario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("organizacion/{organizacionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CertificadoRespuestaDTO>>> ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                var certificados = await _obtenerPorOrganizacionHandler.Ejecutar(organizacionId);
                return Ok(certificados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener certificados por organización");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/descargar")]
        public async Task<IActionResult> DescargarCertificado(int id)
        {
            try
            {
                var pdfBytes = await _descargarHandler.Ejecutar(id);
                if (pdfBytes == null || pdfBytes.Length == 0)
                    return NotFound();

                return File(pdfBytes, "application/pdf", $"certificado-{id}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al descargar certificado");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
