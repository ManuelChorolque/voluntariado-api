using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Horas
{
    public class EliminarHorasHandler
    {
        private readonly IRepositorioHoras _repositorio;

        public EliminarHorasHandler(IRepositorioHoras repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(int id)
        {
            var horas = await _repositorio.ObtenerPorIdAsync(id);
            if (horas == null)
                throw new KeyNotFoundException($"No se encontró el registro de horas con ID {id}");

            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}
