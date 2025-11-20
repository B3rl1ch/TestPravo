using TestTaskPravo.Core.Models;

namespace TestTaskPravo.Core.Database.Abstraction.Providers;

public interface ISectionProvider
{
    Task<List<Section>> GetAllWithTagsAsync(CancellationToken ct);
    Task<Section?> GetByIdWithTagsAsync(Guid id, CancellationToken ct);
    Task AddAsync(Section section, CancellationToken ct);
    Task DeleteAsync(Section section, CancellationToken ct);
}