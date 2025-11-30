using BP.Application.Commons.Bases;
using BP.Application.Dtos.Request;
using BP.Application.Dtos.Response;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;

namespace BP.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<BaseResponse<BaseEntityResponse<ClientResponseDto>>> ListClients(BaseFiltersRequest filters);
        Task<BaseResponse<ClientResponseDto>> ClientById(long id);
        Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto);
        Task<BaseResponse<bool>> EditClient(long clienId,ClientRequestDto client);
        Task<BaseResponse<bool>> RemoveClient(long clientId);
    }
}
