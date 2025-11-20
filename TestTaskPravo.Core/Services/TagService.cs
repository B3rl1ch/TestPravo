using TestTaskPravo.Core.Abstractions;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Exceptions.Models;
using TestTaskPravo.Core.Models;

namespace TestTaskPravo.Core.Services;

public class TagService : ITagService
{
    private readonly ITagProvider _tagProvider;

    public TagService(ITagProvider tagProvider)
    {
        _tagProvider = tagProvider;
    }

    public async Task<Tag> GetOrCreateAsync(string name, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new BaseAppException($"Тег не может быть пустым, пожалуйста введите данные", "TAG_SERVICE_TAG_EMPTY");
        
        var tagNormalized = name.Trim().ToLower();
        
        var existedTag = await _tagProvider.GetByNameAsync(tagNormalized, ct);
        if (existedTag != null) return existedTag;
        
        var result = await _tagProvider.AddAsync(tagNormalized, ct);
        return result;
    }

    public async Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names, CancellationToken ct)
    {
        var result = new List<Tag>();

        foreach (var name in names
                     .Where(x => !string.IsNullOrWhiteSpace(x))
                     .Select(x => x.Trim())
                     .Distinct(StringComparer.OrdinalIgnoreCase))
        {
            result.Add(await GetOrCreateAsync(name, ct));
        }

        return result;
    }
}