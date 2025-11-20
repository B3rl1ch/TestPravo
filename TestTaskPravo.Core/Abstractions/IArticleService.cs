using TestTaskPravo.Core.Models;
using TestTaskPravo.Model.Models;

namespace TestTaskPravo.Core.Abstractions;

public interface IArticleService : IBaseService<Article, ArticleCreateDto, ArticleUpdateDto>
{
}