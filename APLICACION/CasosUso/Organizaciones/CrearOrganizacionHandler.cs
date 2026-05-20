using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Organizaciones;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Organizaciones
{
    public class CrearOrganizacionHandler
    {
        private readonly IRepositorioOrganizaciones _repositorio;
        private readonly IMapper _mapper;

        public CrearOrganizacionHandler(IRepositorioOrganizaciones repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<OrganizacionRespuestaDTO> Ejecutar(CrearOrganizacionDTO dto)
        {
            // Validar nombre
            ValidadorCampos.ValidarCampoRequerido(dto.Nombre, nameof(dto.Nombre));

            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                ValidadorCampos.ValidarEmail(dto.Email);
            }

            // Crear entidad
            var organizacion = new Organizacion
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Telefono = dto.Telefono,
                Email = dto.Email,
                SitioWeb = dto.SitioWeb,
                Direccion = dto.Direccion,
                FechaRegistro = DateTime.UtcNow
            };

            // Guardar
            await _repositorio.AgregarAsync(organizacion);
            await _repositorio.GuardarCambiosAsync();

            // Retornar DTO
            return _mapper.Map<OrganizacionRespuestaDTO>(organizacion);
        }
    }

}
