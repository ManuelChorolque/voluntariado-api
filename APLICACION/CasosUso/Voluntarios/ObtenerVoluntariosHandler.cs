using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Voluntarios;

namespace APLICACION.CasosUso.Voluntarios
{
    public class ObtenerVoluntariosHandler
    {
        private readonly IRepositorioVoluntarios _repositorio;
        private readonly IMapper _mapper;

        public ObtenerVoluntariosHandler(IRepositorioVoluntarios repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<VoluntarioRespuestaDTO>> Ejecutar(int pageNumber = 1, int pageSize = 10, string? busquedaNombre = null, int? organizacionId = null)
        {
            var voluntarios = await _repositorio.ObtenerTodosAsync(pageNumber, pageSize, busquedaNombre, organizacionId);
            return _mapper.Map<List<VoluntarioRespuestaDTO>>(voluntarios);
        }
    }
}
