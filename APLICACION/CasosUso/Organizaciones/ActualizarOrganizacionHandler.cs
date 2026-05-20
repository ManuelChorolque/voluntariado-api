using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Organizaciones;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Organizaciones
{
    public class ActualizarOrganizacionHandler
    {
        private readonly IRepositorioOrganizaciones _repositorio;
        private readonly IMapper _mapper;

        public ActualizarOrganizacionHandler(IRepositorioOrganizaciones repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<OrganizacionRespuestaDTO> Ejecutar(int id, CrearOrganizacionDTO dto)
        {
            ValidadorCampos.ValidarCampoRequerido(dto.Nombre, nameof(dto.Nombre));

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                ValidadorCampos.ValidarEmail(dto.Email);
            }

            var organizacion = await _repositorio.ObtenerPorIdAsync(id);
            if (organizacion == null)
                throw new KeyNotFoundException($"No se encontró la organización con ID {id}");

            _mapper.Map(dto, organizacion);
            await _repositorio.ActualizarAsync(organizacion);
            await _repositorio.GuardarCambiosAsync();

            return _mapper.Map<OrganizacionRespuestaDTO>(organizacion);
        }
    }
}
