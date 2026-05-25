using APLICACION.CasosUso.Auth;
using APLICACION.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterHandler _registerHandler;
        private readonly LoginHandler _loginHandler;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            RegisterHandler registerHandler,
            LoginHandler loginHandler,
            ILogger<AuthController> logger)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
            _logger = logger;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<AuthResponseDTO>> Registrar([FromBody] RegisterRequestDTO dto)
        {
            try
            {
                var respuesta = await _registerHandler.Ejecutar(dto);
                return Ok(respuesta);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar usuario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO dto)
        {
            try
            {
                var respuesta = await _loginHandler.Ejecutar(dto);
                return Ok(respuesta);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al iniciar sesion");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
