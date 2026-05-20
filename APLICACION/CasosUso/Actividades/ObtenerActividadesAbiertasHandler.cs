using AutoMapper;
using DOMINIO.Interfaces;
using APLICACION.DTOs.Actividadades;

namespace APLICACION.CasosUso.Actividades
{
    public class ObtenerActividadesAbiertasHandler
    {
        private readonly IRepositorioActividades _repositorio;
        private readonly IMapper _mapper;

        public ObtenerActividadesAbiertasHandler(IRepositorioActividades repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<ActividadRespuestaDTO>> Ejecutar()
        {
            var actividades = await _repositorio.ObtenerActividadesAbiertasAsync();
            return _mapper.Map<List<ActividadRespuestaDTO>>(actividades);
        }
    }
}
