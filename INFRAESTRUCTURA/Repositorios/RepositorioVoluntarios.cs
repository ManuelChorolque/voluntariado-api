using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using Infraestructura.Data;

namespace INFRAESTRUCTURA.Repositorios
{
    public class RepositorioVoluntarios : IRepositorioVoluntarios
    {
        private readonly VoluntariadoDbContext _contexto;

        public RepositorioVoluntarios(VoluntariadoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Voluntario>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null, int? organizacionId = null)
        {
            var query = _contexto.Voluntarios
                .Include(v => v.Organizacion)
                .Include(v => v.HorasRegistradas)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(busquedaNombre))
                query = query.Where(v => v.Nombre.Contains(busquedaNombre));

            if (organizacionId.HasValue)
                query = query.Where(v => v.OrganizacionId == organizacionId.Value);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Voluntario?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Voluntarios
                .Include(v => v.Organizacion)
                .Include(v => v.HorasRegistradas)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Voluntario?> ObtenerPorCedulaAsync(string cedula)
        {
            return await _contexto.Voluntarios
                .Include(v => v.Organizacion)
                .FirstOrDefaultAsync(v => v.Cedula == cedula);
        }

        public async Task<Voluntario?> ObtenerPorEmailAsync(string email)
        {
            return await _contexto.Voluntarios
                .Include(v => v.Organizacion)
                .FirstOrDefaultAsync(v => v.Email == email);
        }

        public async Task<IEnumerable<Voluntario>> ObtenerPorOrganizacionAsync(int organizacionId)
        {
            return await _contexto.Voluntarios
                .Where(v => v.OrganizacionId == organizacionId)
                .Include(v => v.HorasRegistradas)
                .ToListAsync();
        }

        public async Task AgregarAsync(Voluntario voluntario)
        {
            _contexto.Voluntarios.Add(voluntario);
        }

        public async Task ActualizarAsync(Voluntario voluntario)
        {
            _contexto.Voluntarios.Update(voluntario);
        }

        public async Task EliminarAsync(int id)
        {
            var voluntario = await _contexto.Voluntarios.FindAsync(id);
            if (voluntario != null)
            {
                _contexto.Voluntarios.Remove(voluntario);
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _contexto.Voluntarios.AnyAsync(v => v.Id == id);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
