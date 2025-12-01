using AutoMapper;
using BP.Appication.Dtos.Request;
using BP.Appication.Dtos.Response;
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
                .ForMember(x => x.ClienteId,  x => x.MapFrom(y => y.PersonaId))
                .ReverseMap();
            CreateMap<BaseEntityResponse<Cliente>, BaseEntityResponse<ClientResponseDto>>()
                .ReverseMap();

            CreateMap<ClientRequestDto, Cliente>()
                .ReverseMap();           

        }
    }
}
