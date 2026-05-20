using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Voluntarios
{
    public class EliminarVoluntarioHandler
    {
        private readonly IRepositorioVoluntarios _repositorio;

        public EliminarVoluntarioHandler(IRepositorioVoluntarios repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(int id)
        {
            // Obtener voluntario
            var voluntario = await _repositorio.ObtenerPorIdAsync(id);
            if (voluntario == null)
            {
                throw new KeyNotFoundException($"No se encontró el voluntario con ID {id}");
            }

            // Eliminar
            await _repositorio.EliminarAsync(voluntario.Id);
            await _repositorio.GuardarCambiosAsync();

            return true;
        }
    }
}
