using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Voluntarios;

namespace APLICACION.CasosUso.Voluntarios
{
    public class ObtenerVoluntarioPorIdHandler
    {
        private readonly IRepositorioVoluntarios _repositorio;
        private readonly IMapper _mapper;

        public ObtenerVoluntarioPorIdHandler(IRepositorioVoluntarios repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<VoluntarioRespuestaDTO?> Ejecutar(int id)
        {
            var voluntario = await _repositorio.ObtenerPorIdAsync(id);
            return voluntario == null ? null : _mapper.Map<VoluntarioRespuestaDTO>(voluntario);
        }
    }
}
