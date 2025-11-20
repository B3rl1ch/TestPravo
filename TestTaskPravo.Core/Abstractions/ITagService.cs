using TestTaskPravo.Core.Models;

namespace TestTaskPravo.Core.Abstractions;

public interface ITagService
{
    Task<Tag> GetOrCreateAsync(string normalizedName, CancellationToken ct);
    Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names, CancellationToken ct);
}