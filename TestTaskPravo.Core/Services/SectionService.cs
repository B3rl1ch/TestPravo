using AutoMapper;
using TestTaskPravo.Core.Abstractions;
using TestTaskPravo.Core.Database.Abstraction;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Models;

namespace TestTaskPravo.Core.Services;

public class SectionService : ISectionService
{
    private readonly IUnitOfWork _uow;
    private readonly ISectionProvider _sectionProvider;
    private readonly IArticleProvider _articleProvider;
    private readonly IMapper _mapper;

    public SectionService(IUnitOfWork uow, ISectionProvider sectionProvider, IArticleProvider articleProvider, IMapper mapper)
    {
        _uow = uow;
        _sectionProvider = sectionProvider;
        _articleProvider = articleProvider;
        _mapper = mapper;
    }

    public async Task<List<Section>> GetSectionsAsync(CancellationToken ct)
    {
        var sections = await _sectionProvider.GetAllWithTagsAsync(ct);
        var articles = await _articleProvider.ListAsync(ct);

        var result = new List<Section>();

        foreach (var sec in sections)
        {
            var secTagIds = sec.Tags
                .Select(t => t.TagId)
                .Distinct()
                .ToHashSet();
            
            sec.Articles = articles
                .Where(a => TagSetEquals(a, secTagIds)).ToList();
            
            result.Add(sec);
        }

        return result
            .OrderByDescending(x => x.Articles.Count)
            .ToList();
    }

    public async Task<List<Article>> GetSectionArticlesAsync(Guid sectionId, CancellationToken ct)
    {
        var sections = await _sectionProvider.GetAllWithTagsAsync(ct);
        var section = sections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
            throw new KeyNotFoundException($"Раздел с таким ID {sectionId} не найден");

        var secTagIds = section.Tags
            .Select(t => t.TagId)
            .Distinct()
            .ToHashSet();

        var articles = await _articleProvider.ListAsync(ct);

        var filtered = articles
            .Where(a => TagSetEquals(a, secTagIds))
            .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)
            .ToList();

        return filtered;
    }

    public async Task RebuildSectionsAsync(CancellationToken ct)
    {
        var existingSections = await _sectionProvider.GetAllWithTagsAsync(ct);
        var articles = await _articleProvider.ListAsync(ct);
        
        var groups = articles
            .GroupBy(BuildTagKeyForArticle)
            .ToList();

        var groupKeys = groups
            .Select(g => g.Key)
            .ToHashSet(StringComparer.Ordinal);

        await DeleteSection(existingSections, groupKeys, ct);

        var existingKeys = existingSections
            .ToDictionary(BuildTagKeyForSection, s => s, StringComparer.Ordinal);

        await AddSection(groups, existingKeys, ct);
    }

    private async Task DeleteSection(List<Section> sections, HashSet<string> groupKeys, CancellationToken ct)
    {
        foreach (var sec in sections.ToList())
        {
            var secKey = BuildTagKeyForSection(sec);
            if (!groupKeys.Contains(secKey))
            {
                await _sectionProvider.DeleteAsync(sec, ct);
                sections.Remove(sec);
            }
        }
    }

    private async Task AddSection(List<IGrouping<string,Article>> groups,  
        Dictionary<string,Section> existingKeys, 
        CancellationToken ct)
    {
        foreach (var g in groups)
        {
            var key = g.Key;
            if (existingKeys.ContainsKey(key))
                continue;

            var firstArticle = g.First();
            var tags = firstArticle.Tags
                .Select(t => t.Tag)
                .DistinctBy(t => t.Id)
                .ToList();

            var title = BuildSectionTitle(tags.Select(t => t.Name));

            var section = new Section
            {
                Id = Guid.NewGuid(),
                Title = title
            };

            foreach (var tag in tags)
            {
                section.Tags.Add(new SectionTag
                {
                    TagId = tag.Id
                });
            }

            await _sectionProvider.AddAsync(section, ct);
        }
    }

    private static bool TagSetEquals(Article article, HashSet<Guid> sectionTagIds)
    {
        var articleTagIds = article.Tags
            .Select(t => t.TagId)
            .Distinct()
            .ToHashSet();

        return articleTagIds.SetEquals(sectionTagIds);
    }

    private static string BuildTagKeyForArticle(Article article)
    {
        var names = article.Tags
            .Select(t => t.Tag.Name)
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(n => n, StringComparer.OrdinalIgnoreCase);

        return string.Join(",", names);
    }

    private static string BuildTagKeyForSection(Section section)
    {
        var names = section.Tags
            .Select(t => t.Tag.Name)
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(n => n, StringComparer.OrdinalIgnoreCase);

        return string.Join(",", names);
    }

    private static string BuildSectionTitle(IEnumerable<string> tagNames)
    {
        var joined = string.Join(",", tagNames);
        return joined.Length > 1024 ? joined[..1024] : joined;
    }
}