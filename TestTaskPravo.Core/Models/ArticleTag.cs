namespace TestTaskPravo.Core.Models;

/// <summary>
/// Класс для описания тегов статьи
/// </summary>
public class ArticleTag
{
    /// <summary>
    /// ID статьи
    /// </summary>
    public Guid ArticleId { get; set; }
    
    /// <summary>
    /// Статья
    /// </summary>
    public Article Article { get; set; } = null!;
    
    /// <summary>
    /// ID тэга
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// Тэг
    /// </summary>
    public Tag Tag { get; set; } = null!;

    /// <summary>
    /// Порядок сортировки
    /// </summary>
    public int Order { get; set; }
}