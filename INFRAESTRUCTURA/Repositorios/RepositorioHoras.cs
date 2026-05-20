using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using Infraestructura.Data;

namespace INFRAESTRUCTURA.Repositorios
{
    public class RepositorioHoras : IRepositorioHoras
    {
        private readonly VoluntariadoDbContext _contexto;

        public RepositorioHoras(VoluntariadoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<HorasVoluntariado>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, int? voluntarioId = null, int? actividadId = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var query = _contexto.HorasVoluntariado
                .Include(h => h.Voluntario)
                .Include(h => h.Actividad)
                .AsQueryable();

            if (voluntarioId.HasValue)
                query = query.Where(h => h.VoluntarioId == voluntarioId.Value);

            if (actividadId.HasValue)
                query = query.Where(h => h.ActividadId == actividadId.Value);

            if (fechaInicio.HasValue)
                query = query.Where(h => h.FechaInicio >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(h => h.FechaFin <= fechaFin.Value);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<HorasVoluntariado?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.HorasVoluntariado
                .Include(h => h.Voluntario)
                .Include(h => h.Actividad)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<HorasVoluntariado>> ObtenerPorVoluntarioAsync(int voluntarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var query = _contexto.HorasVoluntariado
                .Where(h => h.VoluntarioId == voluntarioId)
                .Include(h => h.Actividad)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(h => h.FechaInicio >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(h => h.FechaFin <= fechaFin.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<HorasVoluntariado>> ObtenerPorActividadAsync(int actividadId)
        {
            return await _contexto.HorasVoluntariado
                .Where(h => h.ActividadId == actividadId)
                .Include(h => h.Voluntario)
                .ToListAsync();
        }

        public async Task<decimal> ObtenerTotalHorasVoluntarioAsync(int voluntarioId)
        {
            return await _contexto.HorasVoluntariado
                .Where(h => h.VoluntarioId == voluntarioId)
                .SumAsync(h => h.Horas);
        }

        public async Task AgregarAsync(HorasVoluntariado horas)
        {
            _contexto.HorasVoluntariado.Add(horas);
        }

        public async Task ActualizarAsync(HorasVoluntariado horas)
        {
            _contexto.HorasVoluntariado.Update(horas);
        }

        public async Task EliminarAsync(int id)
        {
            var horas = await _contexto.HorasVoluntariado.FindAsync(id);
            if (horas != null)
            {
                _contexto.HorasVoluntariado.Remove(horas);
            }
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
