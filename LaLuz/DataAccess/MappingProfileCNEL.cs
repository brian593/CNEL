using System;
using AutoMapper;
using LaLuz.Models;

namespace LaLuz.DataAccess;

public class MappingProfileCNEL: Profile
{
public MappingProfileCNEL()
    {
        CreateMap<Corte, Notificacion>()
            .ForMember(dest => dest.cuentaContrato, opt => opt.MapFrom(src => src.CuentaContrato))
            .ForMember(dest => dest.alimentador, opt => opt.MapFrom(src => src.Alimentador))
            .ForMember(dest => dest.cuen, opt => opt.MapFrom(src => src.CUEN))
            .ForMember(dest => dest.direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.detalleplanificacion, opt => opt.MapFrom(src => src.Cortes));

        CreateMap<CorteDetalle, DetallePlanificacion>()
            .ForMember(dest => dest.fechaCorte, opt => opt.MapFrom(src => src.FechaEjecucion))
            .ForMember(dest => dest.horaDesde, opt => opt.MapFrom(src => src.HoraInicio))
            .ForMember(dest => dest.horaHasta, opt => opt.MapFrom(src => src.HoraFin))
            .ForMember(dest => dest.comentario, opt => opt.MapFrom(src => src.Detalle))
            .ForMember(dest => dest.fechaHoraCorte, opt => opt.MapFrom(src => src.FechaEjecucion));
    }
}
