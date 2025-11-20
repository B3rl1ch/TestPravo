using TestTaskPravo.Core.Models;

namespace TestTaskPravo.Core.Abstractions;

public interface ISectionService
{
    Task<List<Section>> GetSectionsAsync(CancellationToken ct);
    Task<List<Article>> GetSectionArticlesAsync(Guid sectionId, CancellationToken ct);
    Task RebuildSectionsAsync(CancellationToken ct);
}