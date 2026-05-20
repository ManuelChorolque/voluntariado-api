using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using Infraestructura.Data;

namespace INFRAESTRUCTURA.Repositorios
{
    public class RepositorioCertificados : IRepositorioCertificados
    {
        private readonly VoluntariadoDbContext _contexto;

        public RepositorioCertificados(VoluntariadoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Certificado>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _contexto.Certificados
                .Include(c => c.Voluntario)
                .Include(c => c.Organizacion)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Certificado?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Certificados
                .Include(c => c.Voluntario)
                .Include(c => c.Organizacion)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Certificado>> ObtenerPorVoluntarioAsync(int voluntarioId)
        {
            return await _contexto.Certificados
                .Where(c => c.VoluntarioId == voluntarioId)
                .Include(c => c.Organizacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Certificado>> ObtenerPorOrganizacionAsync(int organizacionId)
        {
            return await _contexto.Certificados
                .Where(c => c.OrganizacionId == organizacionId)
                .Include(c => c.Voluntario)
                .ToListAsync();
        }

        public async Task<Certificado?> ObtenerPorNumeroSerieAsync(string numeroSerie)
        {
            return await _contexto.Certificados
                .Include(c => c.Voluntario)
                .Include(c => c.Organizacion)
                .FirstOrDefaultAsync(c => c.NumeroSerie == numeroSerie);
        }

        public async Task AgregarAsync(Certificado certificado)
        {
            _contexto.Certificados.Add(certificado);
        }

        public async Task ActualizarAsync(Certificado certificado)
        {
            _contexto.Certificados.Update(certificado);
        }

        public async Task EliminarAsync(int id)
        {
            var certificado = await _contexto.Certificados.FindAsync(id);
            if (certificado != null)
            {
                _contexto.Certificados.Remove(certificado);
            }
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
