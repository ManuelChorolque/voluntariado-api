using System.Security.Claims;

namespace API.Services
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UsuarioId => int.Parse(
            _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public string Email => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email)!;

        public string NombreCompleto => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.GivenName)!;

        public string Rol => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Role)!;

        public int? VoluntarioId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("VoluntarioId");
                return value is not null ? int.Parse(value) : null;
            }
        }

        public int? OrganizacionId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("OrganizacionId");
                return value is not null ? int.Parse(value) : null;
            }
        }

        public bool EsAdmin => Rol == "Admin";
        public bool EsOrganizacion => Rol == "Organizacion";
        public bool EsVoluntario => Rol == "Voluntario";
    }
}
