using DOMINIO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMINIO.Interfaces
{
    public interface IRepositorioVoluntarios
    {
        Task<IEnumerable<Voluntario>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null, int? organizacionId = null);
        Task<Voluntario?> ObtenerPorIdAsync(int id);
        Task<Voluntario?> ObtenerPorCedulaAsync(string cedula);
        Task<Voluntario?> ObtenerPorEmailAsync(string email);
        Task<IEnumerable<Voluntario>> ObtenerPorOrganizacionAsync(int organizacionId);
        Task AgregarAsync(Voluntario voluntario);
        Task ActualizarAsync(Voluntario voluntario);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task GuardarCambiosAsync();
    }
}
