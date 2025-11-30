using AutoMapper;
using BP.Application.Dtos.Request;
using BP.Application.Dtos.Response;
using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Response;

namespace BP.Application.Mappers
{
    public class ClientMappingsProfile : Profile
    {
        public ClientMappingsProfile()
        {
            CreateMap<Cliente, ClientResponseDto>()
                .ForMember(x => x.EstadoCliente, x => x.MapFrom(y => y.Estado.Equals(true) ? "Activo" : "Inactivo"))
                .ReverseMap();
            CreateMap<BaseEntityResponse<Cliente>, BaseEntityResponse<ClientResponseDto>>()
                .ReverseMap();

            CreateMap<ClientRequestDto, Cliente>()
                .ReverseMap();

        }
    }
}
