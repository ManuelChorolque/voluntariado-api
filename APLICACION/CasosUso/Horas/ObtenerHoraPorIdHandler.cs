using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Horas;

namespace APLICACION.CasosUso.Horas
{
    public class ObtenerHoraPorIdHandler
    {
        private readonly IRepositorioHoras _repositorio;
        private readonly IMapper _mapper;

        public ObtenerHoraPorIdHandler(IRepositorioHoras repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<HorasRespuestaDTO?> Ejecutar(int id)
        {
            var horas = await _repositorio.ObtenerPorIdAsync(id);
            return horas == null ? null : _mapper.Map<HorasRespuestaDTO>(horas);
        }
    }
}
