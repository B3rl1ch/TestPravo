using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Exceptions.Models;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Data.Database;
using TestTaskPravo.Data.Database.Abstraction;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Providers;

public class ArticleProvider : BaseProvider<Article, ArticleDbo>, IArticleProvider
{
    public ArticleProvider(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public override async Task<Article?> GetAsync(Guid key, CancellationToken ct)
    {
        var item = await DbContext.Articles
            .Include(x => x.Tags).ThenInclude(x => x.Tag)
            .FirstOrDefaultAsync(x => x.Id == key, cancellationToken: ct);
        
        return Mapper.Map<Article>(item);
    }

    public override async Task<List<Article>> ListAsync(CancellationToken ct)
    {
        var items = await DbContext.Articles
            .Include(x => x.Tags).ThenInclude(x => x.Tag)
            .ToListAsync(ct);
        
        return Mapper.Map<List<Article>>(items);
    }
    
    public override async Task UpdateAsync(Article data, CancellationToken ct)
    {
        var dbo = await DbContext.Articles
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == data.Id, ct);

        if (dbo == null)
            throw new BaseAppException("Не найдено статьи по ID " +  data.Id, "ARTICLE_PROVIDER_FIND_ERROR");

        dbo.Title = data.Title;
        dbo.UpdatedAt = data.UpdatedAt;

        DbContext.ArticleTags.RemoveRange(dbo.Tags);

        var newTags = data.Tags
            .Select(t => new ArticleTagDbo
            {
                ArticleId = data.Id,
                TagId = t.TagId,
                Order = t.Order
            })
            .ToList();

        await DbContext.ArticleTags.AddRangeAsync(newTags, ct);
    }
}