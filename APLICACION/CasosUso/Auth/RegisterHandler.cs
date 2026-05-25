using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Enumeradores;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Auth;
using APLICACION.Interfaces;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Auth
{
    public class RegisterHandler
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITokenServicio _tokenServicio;

        public RegisterHandler(IUsuarioRepositorio usuarioRepositorio, ITokenServicio tokenServicio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _tokenServicio = tokenServicio;
        }

        public async Task<AuthResponseDTO> Ejecutar(RegisterRequestDTO dto)
        {
            ValidadorCampos.ValidarCampoRequerido(dto.Nombre, nameof(dto.Nombre));
            ValidadorCampos.ValidarCampoRequerido(dto.Apellido, nameof(dto.Apellido));
            ValidadorCampos.ValidarEmail(dto.Email);
            ValidadorCampos.ValidarCampoRequerido(dto.Password, nameof(dto.Password));

            if (dto.Password.Length < 6)
                throw new InvalidOperationException("La contraseña debe tener al menos 6 caracteres.");

            if (!RolValido(dto.Rol))
                throw new InvalidOperationException("Rol inválido. Use 'Voluntario' u 'Organizacion'.");

            var emailExiste = await _usuarioRepositorio.ExisteEmailAsync(dto.Email);
            if (emailExiste)
                throw new InvalidOperationException("Ya existe un usuario con este email.");

            var rol = dto.Rol == "Organizacion" ? RolUsuario.Organizacion : RolUsuario.Voluntario;

            var usuario = new Usuario
            {
                Nombre = dto.Nombre.Trim(),
                Apellido = dto.Apellido.Trim(),
                Email = dto.Email.Trim().ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = rol,
                FechaRegistro = DateTime.UtcNow,
                Activo = true
            };

            await _usuarioRepositorio.AgregarAsync(usuario);
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

        private static bool RolValido(string rol)
        {
            return rol is "Voluntario" or "Organizacion";
        }
    }
}
