using BP.Infrastructure.Persistences.Contexts;
using BP.Infrastructure.Persistences.Interfaces;

namespace BP.Infrastructure.Persistences.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly BpContext _context;
        public IClientRepository Cliente { get; private set; }

        public UnitOfWork(BpContext context)
        {
            _context = context;
            Cliente = new ClientRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
