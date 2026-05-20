using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Voluntarios;
using APLICACION.Utilidades;

namespace APLICACION.CasosUso.Voluntarios
{
    public class ActualizarVoluntarioHandler
    {
        private readonly IRepositorioVoluntarios _repositorio;
        private readonly IMapper _mapper;

        public ActualizarVoluntarioHandler(IRepositorioVoluntarios repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<VoluntarioRespuestaDTO> Ejecutar(int id, ActualizarVoluntarioDTO dto)
        {
            // Validar campos obligatorios
            ValidadorCampos.ValidarCampoRequerido(dto.Nombre, nameof(dto.Nombre));
            ValidadorCampos.ValidarEmail(dto.Email);
            ValidadorCampos.ValidarTelefono(dto.Telefono);

            // Obtener voluntario existente
            var voluntario = await _repositorio.ObtenerPorIdAsync(id);
            if (voluntario == null)
            {
                throw new KeyNotFoundException($"No se encontró el voluntario con ID {id}");
            }

            // Verificar que el nuevo email no sea usado por otro voluntario
            if (voluntario.Email != dto.Email)
            {
                var voluntarioExistente = await _repositorio.ObtenerPorEmailAsync(dto.Email);
                if (voluntarioExistente != null)
                {
                    throw new InvalidOperationException("Ya existe otro voluntario con este email.");
                }
            }

            // Actualizar datos
            voluntario.Nombre = dto.Nombre;
            voluntario.Apellido = dto.Apellido;
            voluntario.Email = dto.Email;
            voluntario.Telefono = dto.Telefono;
            voluntario.Direccion = dto.Direccion;
            voluntario.Cedula = dto.Cedula;
            voluntario.FechaNacimiento = dto.FechaNacimiento;

            // Guardar cambios
            await _repositorio.ActualizarAsync(voluntario);
            await _repositorio.GuardarCambiosAsync();

            // Retornar DTO
            return _mapper.Map<VoluntarioRespuestaDTO>(voluntario);
        }
    }

}
