using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Actividades
{
    public class EliminarActividadHandler
    {
        private readonly IRepositorioActividades _repositorio;

        public EliminarActividadHandler(IRepositorioActividades repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(int id)
        {
            var existe = await _repositorio.ExisteAsync(id);
            if (!existe)
                throw new KeyNotFoundException($"No se encontró la actividad con ID {id}");

            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}
