using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using BP.Infrastructure.Persistences.Contexts;
using BP.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BP.Infrastructure.Persistences.Repositories
{
    internal class ClientRepository : GenericRepository<Cliente>, IClientRepository
    {
        private readonly BpContext _context;

        public ClientRepository(BpContext context)
        {
            _context = context;
        }


        public async Task<BaseEntityResponse<Cliente>> ListClients(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Cliente>();
            var clients = _context.Clientes.AsNoTracking().AsQueryable();

            if (filters != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                clients = clients.Where(c => c.Nombre.Contains(filters.TextFilter));
            }
            if (filters.Sort == null || filters.Sort == "") filters.Sort = "PersonaId";

            response.TotalRecords = await clients.CountAsync();
            response.Items = await Ordering(filters, clients, !(bool)filters.DownLoad).ToListAsync();
            return response;
        }

        public async Task<Cliente> ClienteById(long clientId)
        {
            var client = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.PersonaId.Equals(clientId));
            return client!;
        }
        public async Task<bool> RegisterClient(Cliente client)
        {
            _context.Add(client);
            var recordsAfected = await _context.SaveChangesAsync();
            return recordsAfected > 0;
        }
        public async Task<bool> EditClient(Cliente client)
        {
            var existing = await _context.Clientes.FirstOrDefaultAsync(c => c.PersonaId == client.PersonaId);

            if (existing == null)
                return false;

            // Cliente
            existing.Contrasena = client.Contrasena;
            existing.Estado = client.Estado;

            // Persona
            existing.Nombre = client.Nombre;
            existing.Genero = client.Genero;
            existing.Edad = client.Edad;
                        
            var recordsAfected = await _context.SaveChangesAsync();
            return recordsAfected > 0;

        }
        public async Task<bool> RemoveClient(long clientId)
        {
            var client = await _context.Clientes.AsNoTracking().SingleOrDefaultAsync(c => c.PersonaId.Equals(clientId));

            if (client != null)
            {
                _context.Remove(client);
            }
            var recordsAfected = await _context.SaveChangesAsync();
            return recordsAfected > 0;
        }
    }
}
