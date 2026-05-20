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
            // Validar que la actividad existe
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(actividadId);
            if (actividad == null)
            {
                throw new KeyNotFoundException($"No se encontró la actividad con ID {actividadId}");
            }

            // Validar que el voluntario existe
            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(voluntarioId);
            if (voluntario == null)
            {
                throw new KeyNotFoundException($"No se encontró el voluntario con ID {voluntarioId}");
            }

            // Validar que no haya alcanzado el número máximo de voluntarios
            if (actividad.VoluntariosAsignados.Count >= actividad.VoluntariosRequeridos)
            {
                throw new InvalidOperationException("La actividad ha alcanzado el número máximo de voluntarios requeridos");
            }

            // Validar que el voluntario no esté ya asignado
            if (actividad.VoluntariosAsignados.Any(v => v.Id == voluntarioId))
            {
                throw new InvalidOperationException("El voluntario ya está asignado a esta actividad");
            }

            // Asignar voluntario
            actividad.VoluntariosAsignados.Add(voluntario);

            // Guardar cambios
            await _repositorioActividades.ActualizarAsync(actividad);
            await _repositorioActividades.GuardarCambiosAsync();

            return true;
        }
    }

}
