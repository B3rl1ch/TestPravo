namespace TestTaskPravo.Core.Models;

/// <summary>
/// Класс для описания разделов
/// </summary>
public class Section
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Список статей
    /// </summary>
    public List<Article> Articles { get; set; } = new();
    
    /// <summary>
    /// Список тегов
    /// </summary>
    public List<SectionTag> Tags { get; set; } = new();
}