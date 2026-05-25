using DOMINIO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMINIO.Interfaces
{
    public interface IRepositorioActividades
    {
        Task<IEnumerable<Actividad>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, int? organizacionId = null, string? busquedaNombre = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null);
        Task<Actividad?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Actividad>> ObtenerPorOrganizacionAsync(int organizacionId);
        Task<IEnumerable<Actividad>> ObtenerActividadesAbiertasAsync(int? excluirVoluntarioId = null);
        Task<IEnumerable<Actividad>> ObtenerPorVoluntarioAsync(int voluntarioId);
        Task AgregarAsync(Actividad actividad);
        Task ActualizarAsync(Actividad actividad);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task GuardarCambiosAsync();
    }

}
