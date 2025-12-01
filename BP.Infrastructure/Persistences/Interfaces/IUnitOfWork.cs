namespace BP.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        // declaracion de interfaces a nivel de repositorio
        IClientRepository Cliente { get; }
        IAccountRepository Cuenta { get; }

        IMovementRepository Movimiento { get; }

        void SaveChanges();
        Task SaveChangesAsync();

    }
}
