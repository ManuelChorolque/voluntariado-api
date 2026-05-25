using DOMINIO.Enumeradores;
using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Actividades
{
    public class AsignarVoluntarioHandler
    {
        private readonly IRepositorioActividades _repositorioActividades;
        private readonly IRepositorioVoluntarios _repositorioVoluntarios;

        public AsignarVoluntarioHandler(
            IRepositorioActividades repositorioActividades,
            IRepositorioVoluntarios repositorioVoluntarios)
        {
            _repositorioActividades = repositorioActividades;
            _repositorioVoluntarios = repositorioVoluntarios;
        }

        public async Task<bool> Ejecutar(int actividadId, int voluntarioId)
        {
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(actividadId)
                ?? throw new KeyNotFoundException($"No se encontró la actividad con ID {actividadId}");

            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(voluntarioId)
                ?? throw new KeyNotFoundException($"No se encontró el voluntario con ID {voluntarioId}");

            if (actividad.VoluntariosAsignados.Count >= actividad.VoluntariosRequeridos)
                throw new InvalidOperationException("La actividad ha alcanzado el número máximo de voluntarios requeridos");

            if (actividad.VoluntariosAsignados.Any(v => v.Id == voluntarioId))
                throw new InvalidOperationException("El voluntario ya está asignado a esta actividad");

            actividad.VoluntariosAsignados.Add(voluntario);

            // Si se llenaron los cupos, cerrar la actividad automáticamente
            if (actividad.VoluntariosAsignados.Count >= actividad.VoluntariosRequeridos)
                actividad.Estado = EstadoActividad.Cerrada;

            await _repositorioActividades.GuardarCambiosAsync();

            return true;
        }
    }
}
