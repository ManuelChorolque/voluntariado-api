using Microsoft.EntityFrameworkCore;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using Infraestructura.Data;

namespace INFRAESTRUCTURA.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly VoluntariadoDbContext _contexto;

        public UsuarioRepositorio(VoluntariadoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Usuarios
                .Include(u => u.Voluntario)
                .Include(u => u.Organizacion)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _contexto.Usuarios
                .Include(u => u.Voluntario)
                .Include(u => u.Organizacion)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _contexto.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task AgregarAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
