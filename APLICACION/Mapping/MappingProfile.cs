namespace Aplicacion.Mapping;

using AutoMapper;
using APLICACION.DTOs.Actividadades;
using APLICACION.DTOs.Certificados;
using APLICACION.DTOs.Horas;
using APLICACION.DTOs.Organizaciones;
using APLICACION.DTOs.Voluntarios;
using DOMINIO.Entidades;
using DOMINIO.Enumerados;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeos para Voluntarios
        CreateMap<CrearVoluntarioDTO, Voluntario>();
        CreateMap<ActualizarVoluntarioDTO, Voluntario>();
        CreateMap<Voluntario, VoluntarioRespuestaDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()))
            .ForMember(dest => dest.Cedula, opt => opt.MapFrom(src => src.Cedula))
            .ForMember(dest => dest.HorasTotales, opt => opt.MapFrom(src => src.HorasTotales))
            .ForMember(dest => dest.NombreOrganizacion, opt => opt.MapFrom(src => src.Organizacion != null ? src.Organizacion.Nombre : null));

        // Mapeos para Horas
        CreateMap<RegistrarHorasDTO, HorasVoluntariado>();
        CreateMap<HorasVoluntariado, HorasRespuestaDTO>()
            .ForMember(dest => dest.Horas, opt => opt.MapFrom(src => src.Horas))
            .ForMember(dest => dest.FechaActividad, opt => opt.MapFrom(src => src.FechaActividad))
            .ForMember(dest => dest.NombreVoluntario, opt => opt.MapFrom(src => src.Voluntario.Nombre + " " + src.Voluntario.Apellido))
            .ForMember(dest => dest.NombreActividad, opt => opt.MapFrom(src => src.Actividad.Nombre));

        // Mapeos para Certificados
        CreateMap<Certificado, CertificadoRespuestaDTO>()
            .ForMember(dest => dest.NombreVoluntario, opt => opt.MapFrom(src => src.Voluntario.Nombre + " " + src.Voluntario.Apellido))
            .ForMember(dest => dest.NombreOrganizacion, opt => opt.MapFrom(src => src.Organizacion.Nombre))
            .ForMember(dest => dest.FechaEmision, opt => opt.MapFrom(src => src.FechaEmision))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()));

        // Mapeos para Organizaciones
        CreateMap<CrearOrganizacionDTO, Organizacion>();
        CreateMap<Organizacion, OrganizacionRespuestaDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.TotalVoluntarios, opt => opt.MapFrom(src => src.Voluntarios.Count))
            .ForMember(dest => dest.TotalActividades, opt => opt.MapFrom(src => src.Actividades.Count));

        // Mapeos para Actividades
        CreateMap<CrearActividadDTO, Actividad>();
        CreateMap<Actividad, ActividadRespuestaDTO>()
            .ForMember(dest => dest.NombreOrganizacion, opt => opt.MapFrom(src => src.Organizacion.Nombre))
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro))
            .ForMember(dest => dest.VoluntariosAsignados, opt => opt.MapFrom(src => src.VoluntariosAsignados.Count))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()));
    }
}

