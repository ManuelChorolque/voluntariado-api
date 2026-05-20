using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using Infraestructura.Data;

namespace INFRAESTRUCTURA.Repositorios
{
    public class RepositorioOrganizaciones : IRepositorioOrganizaciones
    {
        private readonly VoluntariadoDbContext _contexto;

        public RepositorioOrganizaciones(VoluntariadoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Organizacion>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null)
        {
            var query = _contexto.Organizaciones
                .Include(o => o.Voluntarios)
                .Include(o => o.Actividades)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(busquedaNombre))
                query = query.Where(o => o.Nombre.Contains(busquedaNombre));

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Organizacion?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Organizaciones
                .Include(o => o.Voluntarios)
                .Include(o => o.Actividades)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Organizacion?> ObtenerPorNombreAsync(string nombre)
        {
            return await _contexto.Organizaciones
                .Include(o => o.Voluntarios)
                .Include(o => o.Actividades)
                .FirstOrDefaultAsync(o => o.Nombre == nombre);
        }

        public async Task AgregarAsync(Organizacion organizacion)
        {
            _contexto.Organizaciones.Add(organizacion);
        }

        public async Task ActualizarAsync(Organizacion organizacion)
        {
            _contexto.Organizaciones.Update(organizacion);
        }

        public async Task EliminarAsync(int id)
        {
            var organizacion = await _contexto.Organizaciones.FindAsync(id);
            if (organizacion != null)
            {
                _contexto.Organizaciones.Remove(organizacion);
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _contexto.Organizaciones.AnyAsync(o => o.Id == id);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
