using AutoMapper;
using DOMINIO.Entidades;
using DOMINIO.Interfaces;
using DOMINIO.Enumeradores;
using APLICACION.DTOs.Voluntarios;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Voluntarios
{
    public class CrearVoluntarioHandler
    {
        private readonly IRepositorioVoluntarios _repositorio;
        private readonly IMapper _mapper;

        public CrearVoluntarioHandler(IRepositorioVoluntarios repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<VoluntarioRespuestaDTO> Ejecutar(CrearVoluntarioDTO dto)
        {
            // Validar campos obligatorios
            ValidadorCampos.ValidarCampoRequerido(dto.Nombre, nameof(dto.Nombre));
            ValidadorCampos.ValidarEmail(dto.Email);
            ValidadorCampos.ValidarTelefono(dto.Telefono);

            // Verificar que no exista un voluntario con el mismo email
            var voluntarioExistente = await _repositorio.ObtenerPorEmailAsync(dto.Email);
            if (voluntarioExistente != null)
            {
                throw new InvalidOperationException("Ya existe un voluntario registrado con este email.");
            }

            // Crear la entidad
            var voluntario = new Voluntario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Cedula = dto.Cedula,
                FechaNacimiento = dto.FechaNacimiento,
                FechaRegistro = DateTime.UtcNow,
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 0
            };

            // Guardar en la base de datos
            await _repositorio.AgregarAsync(voluntario);
            await _repositorio.GuardarCambiosAsync();

            // Retornar DTO
            return _mapper.Map<VoluntarioRespuestaDTO>(voluntario);
        }
    }



}
