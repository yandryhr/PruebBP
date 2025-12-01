using AutoMapper;
using BP.Appication.Dtos.Request;
using BP.Appication.Dtos.Response;
using BP.Appication.Interfaces;
using BP.Appication.Validators.Account;
using BP.Application.Commons.Bases;
using BP.Application.Dtos.Response;
using BP.Application.Validators.Client;
using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using BP.Infrastructure.Persistences.Interfaces;
using BP.Utilities.Static;

namespace BP.Appication.Services
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AccountValidator _validationRules;

        public AccountApplication(IUnitOfWork unitOfWork, IMapper mapper, AccountValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<AccountResponseDto>>> ListAccounts(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<AccountResponseDto>>();
            var clients = await _unitOfWork.Cuenta.ListAccounts(filters);
            if (clients is not null)
            {
                response.Success = true;
                response.Data = _mapper.Map<BaseEntityResponse<AccountResponseDto>>(clients);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        // Obtener cuenta por número
        public async Task<BaseResponse<AccountResponseDto>> AccountByNum(long numeroCuenta)
        {
            var response = new BaseResponse<AccountResponseDto>();
            var account = await _unitOfWork.Cuenta.AccountByNum(numeroCuenta);
            if (account is not null)
            {
                response.Success = true;
                response.Data = _mapper.Map<AccountResponseDto>(account);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        // Registrar nueva cuenta
        public async Task<BaseResponse<bool>> RegisterAccount(AccountRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
            }

            var account = _mapper.Map<Cuenta>(requestDto);
            response.Data = await _unitOfWork.Cuenta.RegisterAccount(account);

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

        // Editar cuenta existente
        public async Task<BaseResponse<bool>> EditAccount(long accountNum, AccountRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var clientEdit = await _unitOfWork.Cuenta.AccountByNum(accountNum);
            if (clientEdit is null)
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            }
            var account = _mapper.Map<Cuenta>(requestDto);
            account.NumeroCuenta = accountNum;
            response.Data = await _unitOfWork.Cuenta.EditAccount(account);

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

        // Eliminar cuenta
        public async Task<BaseResponse<bool>> DeleteAccount(long numeroCuenta)
        {
            var response = new BaseResponse<bool>();
            var accountRemove = await _unitOfWork.Cuenta.AccountByNum(numeroCuenta);
            if (accountRemove is null)
            {
                response.Success = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            }

            response.Data = await _unitOfWork.Cuenta.DeleteAccount(numeroCuenta);

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
