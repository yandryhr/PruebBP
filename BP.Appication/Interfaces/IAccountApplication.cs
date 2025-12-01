using BP.Appication.Dtos.Request;
using BP.Appication.Dtos.Response;
using BP.Application.Commons.Bases;
using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;

namespace BP.Appication.Interfaces
{
    public interface IAccountApplication
    {
        // Listar cuentas con filtros y paginación
        Task<BaseResponse<BaseEntityResponse<AccountResponseDto>>> ListAccounts(BaseFiltersRequest filters);
        // Obtener cuenta por número
        Task<BaseResponse<AccountResponseDto>> AccountByNum(long numeroCuenta);
        // Registrar nueva cuenta
        Task<BaseResponse<bool>> RegisterAccount(AccountRequestDto requestDto);
        // Editar cuenta existente
        Task<BaseResponse<bool>> EditAccount(long accountNum,AccountRequestDto requestDto);
        // Eliminar cuenta
        Task<BaseResponse<bool>> DeleteAccount(long numeroCuenta);
       
    }
}
