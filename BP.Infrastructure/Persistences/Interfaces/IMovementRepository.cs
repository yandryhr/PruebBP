using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;

namespace BP.Infrastructure.Persistences.Interfaces
{
    public interface IMovementRepository
    {
        Task<bool> RegisterMovement(Movimiento movimiento);
        Task<List<EstadoCuentaDto>> AccountStatusReport(AccounStatusRequest filters);

    }
}
