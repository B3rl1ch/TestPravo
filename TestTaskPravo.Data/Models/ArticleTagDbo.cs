namespace TestTaskPravo.Data.Models;

/// <summary>
/// Класс для описания тегов статьи
/// </summary>
public class ArticleTagDbo
{
    /// <summary>
    /// ID статьи
    /// </summary>
    public Guid ArticleId { get; set; }
    
    /// <summary>
    /// Статья
    /// </summary>
    public ArticleDbo Article { get; set; } = null!;
    
    /// <summary>
    /// ID тэга
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// Тэг
    /// </summary>
    public TagDbo Tag { get; set; } = null!;

    public int Order { get; set; }
}