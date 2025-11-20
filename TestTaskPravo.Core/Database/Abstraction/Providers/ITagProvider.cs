using TestTaskPravo.Core.Models;

namespace TestTaskPravo.Core.Database.Abstraction.Providers;

public interface ITagProvider
{
    Task<Tag> AddAsync(string name,  CancellationToken ct);
    Task<Tag?> GetByNameAsync(string normalizedName, CancellationToken ct);
    Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names, CancellationToken ct);
}