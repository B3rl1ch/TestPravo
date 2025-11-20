using System.Runtime.InteropServices.JavaScript;
using TestTaskPravo.Core.Abstractions;
using TestTaskPravo.Core.Database.Abstraction;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Model.Models;

namespace TestTaskPravo.Core.Services;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _uow;
    private readonly ITagService _tagService;
    private readonly ISectionService _sectionService;
    private readonly IArticleProvider _articleProvider;

    public ArticleService(IArticleProvider articleProvider, ITagService tagService, IUnitOfWork uow, ISectionService sectionService)
    {
        _articleProvider = articleProvider;
        _tagService = tagService;
        _sectionService = sectionService;
        _uow = uow;
    }

    public async Task<Article?> GetAsync(Guid id, CancellationToken ct)
    {
        var article =  await _articleProvider.GetAsync(id, ct);
        if (article != null) return SortTagsByOrder(article);
        return null;
    }

    public async Task<List<Article>> ListAsync(CancellationToken ct)
    {
        var items = await _articleProvider.ListAsync(ct);
        return items
            .Select(item => SortTagsByOrder(item))
            .ToList();
    }

    public async Task<Article> AddAsync(ArticleCreateDto data, CancellationToken ct)
    {
        var tags = await _tagService.GetByNamesAsync(data.Tags, ct);
        var now = DateTime.UtcNow;

        var article = new Article
        {
            Id = Guid.NewGuid(),
            Title = data.Title.Trim(),
            CreatedAt = now
        };

        int i = 0;
        foreach (var t in tags)
        {
            article.Tags.Add(new ArticleTag
            {
                TagId = t.Id,
                Tag = t,
                ArticleId =  article.Id,
                Article = article,
                Order = i++
            });
        }

        await _articleProvider.AddAsync(article, ct);
        await _uow.SaveChangesAsync(ct);
        
        await _sectionService.RebuildSectionsAsync(ct);
        await _uow.SaveChangesAsync(ct);

        return SortTagsByOrder(article);
    }

    public async Task<Article?> UpdateAsync(Guid id, ArticleUpdateDto data, CancellationToken ct)
    {
        var article = await _articleProvider.GetAsync(id, ct);
        if (article == null) return null;

        var tags = await _tagService.GetByNamesAsync(data.Tags, ct);

        article.Title = data.Title.Trim();
        article.UpdatedAt = DateTime.UtcNow;
        
        var articleTagPrev = article.Tags.Select(item => item).ToList();
        var articleTagCurr = tags.Select((item, index) => new ArticleTag()
        {
            TagId = item.Id,
            Tag = item,
            ArticleId =  article.Id,
            Article = article,
            Order = index
        }).ToList();
        article.Tags.Clear();
        article.Tags = articleTagCurr;
        
        await _articleProvider.UpdateAsync(article, ct);
        await _uow.SaveChangesAsync(ct);
        
        await _sectionService.RebuildSectionsAsync(ct);
        await _uow.SaveChangesAsync(ct);
        
        return SortTagsByOrder(article);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        await _articleProvider.DeleteAsync(id, ct);
        await _uow.SaveChangesAsync(ct);
        
        await _sectionService.RebuildSectionsAsync(ct);
        await _uow.SaveChangesAsync(ct);
    }

    private Article SortTagsByOrder(Article article)
    {
        article.Tags = article.Tags
            .OrderBy(item => item.Order)
            .ToList();

        return article;
    }
}