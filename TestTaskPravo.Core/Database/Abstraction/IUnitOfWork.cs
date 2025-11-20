namespace TestTaskPravo.Core.Database.Abstraction;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}