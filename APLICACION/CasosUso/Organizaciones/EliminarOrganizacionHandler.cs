using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Organizaciones
{
    public class EliminarOrganizacionHandler
    {
        private readonly IRepositorioOrganizaciones _repositorio;

        public EliminarOrganizacionHandler(IRepositorioOrganizaciones repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(int id)
        {
            var organizacion = await _repositorio.ObtenerPorIdAsync(id);
            if (organizacion == null)
                throw new KeyNotFoundException($"No se encontró la organización con ID {id}");

            await _repositorio.EliminarAsync(organizacion.Id);
            await _repositorio.GuardarCambiosAsync();

            return true;
        }
    }
}
