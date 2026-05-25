using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DOMINIO.Entidades;
using APLICACION.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace INFRAESTRUCTURA.ServiciosExternos
{
    public class TokenServicio : ITokenServicio
    {
        private readonly IConfiguration _configuration;

        public TokenServicio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.GivenName, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            if (usuario.VoluntarioId.HasValue)
                claimsList.Add(new Claim("VoluntarioId", usuario.VoluntarioId.Value.ToString()));

            if (usuario.OrganizacionId.HasValue)
                claimsList.Add(new Claim("OrganizacionId", usuario.OrganizacionId.Value.ToString()));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
