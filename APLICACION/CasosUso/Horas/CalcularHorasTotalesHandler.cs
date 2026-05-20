using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Horas
{
    public class CalcularHorasTotalesHandler
    {
        private readonly IRepositorioHoras _repositorio;
        private readonly IRepositorioVoluntarios _repositorioVoluntarios;

        public CalcularHorasTotalesHandler(
            IRepositorioHoras repositorio,
            IRepositorioVoluntarios repositorioVoluntarios)
        {
            _repositorio = repositorio;
            _repositorioVoluntarios = repositorioVoluntarios;
        }

        public async Task<double> Ejecutar(int voluntarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            // Verificar que el voluntario existe
            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(voluntarioId);
            if (voluntario == null)
            {
                throw new KeyNotFoundException($"No se encontró el voluntario con ID {voluntarioId}");
            }

            // Obtener todas las horas del voluntario
            var horas = await _repositorio.ObtenerPorVoluntarioAsync(voluntarioId, fechaInicio, fechaFin);
            var totalHoras = horas.Sum(h => (double)h.Horas);

            return totalHoras;
        }
    }

}
