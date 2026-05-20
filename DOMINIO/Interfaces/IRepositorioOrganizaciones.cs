using DOMINIO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMINIO.Interfaces
{
    public interface IRepositorioOrganizaciones
    {
        Task<IEnumerable<Organizacion>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null);
        Task<Organizacion?> ObtenerPorIdAsync(int id);
        Task<Organizacion?> ObtenerPorNombreAsync(string nombre);
        Task AgregarAsync(Organizacion organizacion);
        Task ActualizarAsync(Organizacion organizacion);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task GuardarCambiosAsync();
    }
}
