using BP.Appication.Dtos.Request;
using BP.Application.Commons.Bases;
using BP.Application.Dtos.Request;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Appication.Interfaces
{
    public interface IMovementApplication
    {
        Task<BaseResponse<bool>> RegisterMovement(MovementRequestDto requestDto);
        Task<List<EstadoCuentaDto>> AccountStatusPdf(AccounStatusRequest request);
    }
}
