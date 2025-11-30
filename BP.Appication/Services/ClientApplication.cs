using AutoMapper;
using BP.Application.Commons.Bases;
using BP.Application.Dtos.Request;
using BP.Application.Dtos.Response;
using BP.Application.Interfaces;
using BP.Application.Validators.Client;
using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using BP.Infrastructure.Persistences.Interfaces;
using BP.Utilities.Static;

namespace BP.Application.Services
{
    public class ClientApplication : IClientApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClientValidator _validationRules;

        public ClientApplication(IUnitOfWork unitOfWork, IMapper mapper, ClientValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<ClientResponseDto>>> ListClients(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ClientResponseDto>>();
            var clients = await _unitOfWork.Cliente.ListClients(filters);
            if (clients is not null)
            {
                response.Success = true;
                response.Data = _mapper.Map<BaseEntityResponse<ClientResponseDto>>(clients);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        
        public async Task<BaseResponse<ClientResponseDto>> ClientById(long id)
        {
            var response = new BaseResponse<ClientResponseDto>();
            var client = await _unitOfWork.Cliente.ClienteById(id);
            if (client is not null)
            {
                response.Success = true;
                response.Data = _mapper.Map<ClientResponseDto>(client);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;

        }

        public async Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
            }

            var client = _mapper.Map<Cliente>(requestDto);
            response.Data = await _unitOfWork.Cliente.RegisterClient(client);

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
        public async Task<BaseResponse<bool>> EditClient(long clienId, ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var clientEdit = await _unitOfWork.Cliente.ClienteById(clienId);
            if (clientEdit is null)
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
               
            }
            var client = _mapper.Map<Cliente>(requestDto);
            client.PersonaId = clienId;
            response.Data = await _unitOfWork.Cliente.EditClient(client);

            if (response.Data)
            {
                response.Success = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveClient(long clientId)
        {
           var response = new BaseResponse<bool>();
           var clientRemove = await _unitOfWork.Cliente.ClienteById(clientId);
           if (clientRemove is null)
           {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

           }
              
            response.Data =  await _unitOfWork.Cliente.RemoveClient(clientId);

            if (response.Data)
            {
                response.Success = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
                return response;
        }
    }
}
