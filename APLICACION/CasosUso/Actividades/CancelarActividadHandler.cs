using DOMINIO.Enumeradores;
using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Actividades
{
    public class CancelarActividadHandler
    {
        private readonly IRepositorioActividades _repositorioActividades;

        public CancelarActividadHandler(IRepositorioActividades repositorioActividades)
        {
            _repositorioActividades = repositorioActividades;
        }

        public async Task Ejecutar(int id)
        {
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la actividad con ID {id}");

            if (actividad.Estado == EstadoActividad.Completada)
                throw new InvalidOperationException("No se puede cancelar una actividad que ya está completada");

            if (actividad.Estado == EstadoActividad.Cancelada)
                throw new InvalidOperationException("La actividad ya está cancelada");

            actividad.Estado = EstadoActividad.Cancelada;

            await _repositorioActividades.GuardarCambiosAsync();
        }
    }
}
