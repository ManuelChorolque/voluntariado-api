using DOMINIO.Enumeradores;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;

namespace APLICACION.CasosUso.Actividades
{
    public class CompletarActividadHandler
    {
        private readonly IRepositorioActividades _repositorioActividades;
        private readonly IRepositorioCertificados _repositorioCertificados;
        private readonly IRepositorioHoras _repositorioHoras;

        public CompletarActividadHandler(
            IRepositorioActividades repositorioActividades,
            IRepositorioCertificados repositorioCertificados,
            IRepositorioHoras repositorioHoras)
        {
            _repositorioActividades = repositorioActividades;
            _repositorioCertificados = repositorioCertificados;
            _repositorioHoras = repositorioHoras;
        }

        public async Task Ejecutar(int id)
        {
            var actividad = await _repositorioActividades.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la actividad con ID {id}");

            if (actividad.Estado != EstadoActividad.EnProgreso)
                throw new InvalidOperationException("Solo se puede completar una actividad que esté en progreso");

            if (actividad.Organizacion == null)
                throw new InvalidOperationException("La actividad no tiene una organización asignada");

            // Cambiar estado a Completada
            actividad.Estado = EstadoActividad.Completada;
            await _repositorioActividades.GuardarCambiosAsync();

            // Obtener horas registradas en esta actividad (para calcular total por voluntario)
            var horasPorActividad = await _repositorioHoras.ObtenerPorActividadAsync(actividad.Id);
            var horasPorVoluntario = horasPorActividad
                .GroupBy(h => h.VoluntarioId)
                .ToDictionary(g => g.Key, g => g.Sum(h => h.Horas));

            var orgNombre = actividad.Organizacion.Nombre;
            var firmanteNombre = actividad.Organizacion.Contacto;
            var firmanteCargo = $"Representante - {orgNombre}";
            var numeroBase = $"CERT-{DateTime.UtcNow:yyyyMMdd}-";

            var certificados = new List<Certificado>();
            var contador = 1;

            foreach (var voluntario in actividad.VoluntariosAsignados)
            {
                var horasTotales = horasPorVoluntario.GetValueOrDefault(voluntario.Id, 0);

                var certificado = new Certificado
                {
                    VoluntarioId = voluntario.Id,
                    ActividadId = actividad.Id,
                    OrganizacionId = actividad.OrganizacionId,
                    NumeroSerie = $"{numeroBase}{contador:D4}",
                    FechaEmision = DateTime.UtcNow,
                    FechaVencimiento = DateTime.UtcNow.AddYears(2),
                    HorasTotales = horasTotales,
                    TemaEspecifico = $"Participación en {actividad.Nombre}",
                    FirmanteNombre = firmanteNombre,
                    FirmanteCargo = firmanteCargo,
                    Estado = EstadoCertificado.Generado
                };

                certificados.Add(certificado);
                contador++;
            }

            if (certificados.Count > 0)
            {
                foreach (var cert in certificados)
                    await _repositorioCertificados.AgregarAsync(cert);

                await _repositorioCertificados.GuardarCambiosAsync();
            }
        }
    }
}
