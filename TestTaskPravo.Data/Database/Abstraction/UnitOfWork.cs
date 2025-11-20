using TestTaskPravo.Core.Database.Abstraction;

namespace TestTaskPravo.Data.Database.Abstraction;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}