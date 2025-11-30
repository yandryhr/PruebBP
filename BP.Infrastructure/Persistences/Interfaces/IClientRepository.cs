using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;

namespace BP.Infrastructure.Persistences.Interfaces
{
    public interface IClientRepository
    {
        Task<BaseEntityResponse<Cliente>> ListClients(BaseFiltersRequest filters);
        Task<Cliente> ClienteById(long clientId);
        Task<bool> RegisterClient(Cliente client);
        Task<bool> EditClient(Cliente client);
        Task<bool> RemoveClient(long clientId);

    }
}
