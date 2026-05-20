using DOMINIO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMINIO.Interfaces
{
    public interface IRepositorioHoras
    {
        Task<IEnumerable<HorasVoluntariado>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10, int? voluntarioId = null, int? actividadId = null, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<HorasVoluntariado?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<HorasVoluntariado>> ObtenerPorVoluntarioAsync(int voluntarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<IEnumerable<HorasVoluntariado>> ObtenerPorActividadAsync(int actividadId);
        Task<decimal> ObtenerTotalHorasVoluntarioAsync(int voluntarioId);
        Task AgregarAsync(HorasVoluntariado horas);
        Task ActualizarAsync(HorasVoluntariado horas);
        Task EliminarAsync(int id);
        Task GuardarCambiosAsync();
    }
}
