using DOMINIO.Enumeradores;
using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Actividades
{
    public class IniciarActividadHandler
    {
        private readonly IRepositorioActividades _repositorioActividades;

        public IniciarActividadHandler(IRepositorioActividades repositorioActividades)
        {
            _repositorioActividades = repositorioActividades;
        }

        public async Task Ejecutar(int id)
        {
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la actividad con ID {id}");

            if (actividad.Estado != EstadoActividad.Cerrada)
                throw new InvalidOperationException("Solo se puede iniciar una actividad que esté cerrada");

            actividad.Estado = EstadoActividad.EnProgreso;

            await _repositorioActividades.GuardarCambiosAsync();
        }
    }
}
