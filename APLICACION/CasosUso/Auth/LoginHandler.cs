using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Auth;
using APLICACION.Interfaces;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Auth
{
    public class LoginHandler
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITokenServicio _tokenServicio;

        public LoginHandler(IUsuarioRepositorio usuarioRepositorio, ITokenServicio tokenServicio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _tokenServicio = tokenServicio;
        }

        public async Task<AuthResponseDTO> Ejecutar(LoginRequestDTO dto)
        {
            ValidadorCampos.ValidarEmail(dto.Email);
            ValidadorCampos.ValidarCampoRequerido(dto.Password, nameof(dto.Password));

            var usuario = await _usuarioRepositorio.ObtenerPorEmailAsync(dto.Email.Trim().ToLower());
            if (usuario == null)
                throw new UnauthorizedAccessException("Email o contraseña incorrectos.");

            if (!usuario.Activo)
                throw new UnauthorizedAccessException("Cuenta desactivada. Contacte al administrador.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                throw new UnauthorizedAccessException("Email o contraseña incorrectos.");

            usuario.UltimoAcceso = DateTime.UtcNow;
            await _usuarioRepositorio.ActualizarAsync(usuario);
            await _usuarioRepositorio.GuardarCambiosAsync();

            return new AuthResponseDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Rol = usuario.Rol.ToString(),
                Token = _tokenServicio.GenerarToken(usuario),
                VoluntarioId = usuario.VoluntarioId,
                OrganizacionId = usuario.OrganizacionId
            };
        }
    }
}
