using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using DOMINIO.Enumeradores;
using DOMINIO.Interfaces;
using Infraestructura.Data;

namespace INFRAESTRUCTURA.Repositorios
{
    public class RepositorioActividades : IRepositorioActividades
    {
        private readonly VoluntariadoDbContext _contexto;

        public RepositorioActividades(VoluntariadoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Actividad>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, int? organizacionId = null, string? busquedaNombre = null)
        {
            var query = _contexto.Actividades
                .Include(a => a.Organizacion)
                .Include(a => a.VoluntariosAsignados)
                .AsQueryable();

            if (organizacionId.HasValue)
                query = query.Where(a => a.OrganizacionId == organizacionId.Value);

            if (!string.IsNullOrWhiteSpace(busquedaNombre))
                query = query.Where(a => a.Nombre.Contains(busquedaNombre));

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Actividad?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Actividades
                .Include(a => a.Organizacion)
                .Include(a => a.VoluntariosAsignados)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Actividad>> ObtenerPorOrganizacionAsync(int organizacionId)
        {
            return await _contexto.Actividades
                .Where(a => a.OrganizacionId == organizacionId)
                .Include(a => a.VoluntariosAsignados)
                .ToListAsync();
        }

        public async Task<IEnumerable<Actividad>> ObtenerActividadesAbiertasAsync()
        {
            return await _contexto.Actividades
                .Where(a => a.Estado == EstadoActividad.Abierta || a.Estado == EstadoActividad.EnProgreso)
                .Include(a => a.Organizacion)
                .Include(a => a.VoluntariosAsignados)
                .ToListAsync();
        }

        public async Task AgregarAsync(Actividad actividad)
        {
            _contexto.Actividades.Add(actividad);
        }

        public async Task ActualizarAsync(Actividad actividad)
        {
            _contexto.Actividades.Update(actividad);
        }

        public async Task EliminarAsync(int id)
        {
            var actividad = await _contexto.Actividades.FindAsync(id);
            if (actividad != null)
            {
                _contexto.Actividades.Remove(actividad);
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _contexto.Actividades.AnyAsync(a => a.Id == id);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
