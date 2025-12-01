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
    internal class MovementMappingsProfile: Profile
    {
        public MovementMappingsProfile()
        {
                       
            
            CreateMap<MovementRequestDto, Movimiento>()
                .ReverseMap();
        }
    }
}
