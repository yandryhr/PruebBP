using AutoMapper;
using BP.Appication.Dtos.Request;
using BP.Appication.Dtos.Response;
using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Appication.Mappers
{
    public class AccountMappingsProfile: Profile
    {
        public AccountMappingsProfile()
        {
            // Account Mappings
            CreateMap<Cuenta, AccountResponseDto>()
                .ForMember(x => x.EstadoCuenta, x => x.MapFrom(y => y.Estado.Equals(true) ? "Activo" : "Inactivo"))
                .ReverseMap();
            CreateMap<BaseEntityResponse<Cuenta>, BaseEntityResponse<AccountResponseDto>>()
                .ReverseMap();
            CreateMap<AccountRequestDto, Cuenta>()
                .ReverseMap();
        }
    }
}
