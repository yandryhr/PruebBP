using AutoMapper;
using BP.Appication.Dtos.Request;
using BP.Appication.Interfaces;
using BP.Appication.Validators.Movement;
using BP.Application.Commons.Bases;
using BP.Application.Validators.Client;
using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using BP.Infrastructure.Persistences.Interfaces;
using BP.Infrastructure.Persistences.Repositories;
using BP.Utilities.Static;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Appication.Services
{
    public class MovementApplication : IMovementApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MovementValidator _validationRules;

        public MovementApplication(IUnitOfWork unitOfWork, IMapper mapper, MovementValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<bool>> RegisterMovement(MovementRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
            }

            var client = _mapper.Map<Movimiento>(requestDto);
            response.Data = await _unitOfWork.Movimiento.RegisterMovement(client);

            if (response.Data)
            {
                response.Success = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<List<EstadoCuentaDto>> AccountStatusPdf(AccounStatusRequest request)
        {
            var response = new List<EstadoCuentaDto>();
            var data = await _unitOfWork.Movimiento.AccountStatusReport(request);

            if (data != null || data.Any())
            {
                response = _mapper.Map<List<EstadoCuentaDto>>(data);
            }
           return response;
        }
    }
}
