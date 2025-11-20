namespace TestTaskPravo.Core.Models;

/// <summary>
/// Класс для описания статей разделов
/// </summary>
public class Article
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = string.Empty; // max 256
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Теги статьи
    /// </summary>
    public List<ArticleTag> Tags { get; set; } = new();
}