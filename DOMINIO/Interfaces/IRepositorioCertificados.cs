using DOMINIO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMINIO.Interfaces
{
    public interface IRepositorioCertificados
    {
        Task<IEnumerable<Certificado>> ObtenerTodosAsync(int pageNumber = 1, int pageSize = 10);
        Task<Certificado?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Certificado>> ObtenerPorVoluntarioAsync(int voluntarioId);
        Task<IEnumerable<Certificado>> ObtenerPorOrganizacionAsync(int organizacionId);
        Task<Certificado?> ObtenerPorNumeroSerieAsync(string numeroSerie);
        Task AgregarAsync(Certificado certificado);
        Task ActualizarAsync(Certificado certificado);
        Task EliminarAsync(int id);
        Task GuardarCambiosAsync();
    }
}
