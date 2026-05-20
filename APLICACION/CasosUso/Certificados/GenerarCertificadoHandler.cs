using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using DOMINIO.Enumerados;
using APLICACION.DTOs.Certificados;

namespace APLICACION.CasosUso.Certificados
{
    public class GenerarCertificadoHandler
    {
        private readonly IRepositorioCertificados _repositorioCertificados;
        private readonly IRepositorioVoluntarios _repositorioVoluntarios;
        private readonly IRepositorioActividades _repositorioActividades;
        private readonly IMapper _mapper;

        public GenerarCertificadoHandler(
            IRepositorioCertificados repositorioCertificados,
            IRepositorioVoluntarios repositorioVoluntarios,
            IRepositorioActividades repositorioActividades,
            IMapper mapper)
        {
            _repositorioCertificados = repositorioCertificados;
            _repositorioVoluntarios = repositorioVoluntarios;
            _repositorioActividades = repositorioActividades;
            _mapper = mapper;
        }

        public async Task<CertificadoRespuestaDTO> Ejecutar(GenerarCertificadoDTO dto)
        {
            // Validar que el voluntario existe
            var voluntario = await _repositorioVoluntarios.ObtenerPorIdAsync(dto.VoluntarioId);
            if (voluntario == null)
            {
                throw new KeyNotFoundException($"No se encontró el voluntario con ID {dto.VoluntarioId}");
            }

            // Validar que si se proporciona ActividadId, la actividad exista
            if (dto.ActividadId.HasValue)
            {
                var actividad = await _repositorioActividades.ObtenerPorIdAsync(dto.ActividadId.Value);
                if (actividad == null)
                {
                    throw new KeyNotFoundException($"No se encontró la actividad con ID {dto.ActividadId}");
                }
            }

            // Crear certificado
            var certificado = new Certificado
            {
                VoluntarioId = dto.VoluntarioId,
                ActividadId = dto.ActividadId,
                OrganizacionId = dto.OrganizacionId,
                TemaEspecifico = dto.TemaEspecifico,
                FechaEmision = DateTime.UtcNow,
                FechaVencimiento = DateTime.UtcNow.AddYears(2),
                NumeroSerie = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper(),
                HorasTotales = 0,
                Estado = EstadoCertificado.Generado,
                FirmanteNombre = dto.FirmanteNombre,
                FirmanteCargo = dto.FirmanteCargo
            };

            // Guardar
            await _repositorioCertificados.AgregarAsync(certificado);
            await _repositorioCertificados.GuardarCambiosAsync();

            // Retornar DTO
            return _mapper.Map<CertificadoRespuestaDTO>(certificado);
        }
    }
}
