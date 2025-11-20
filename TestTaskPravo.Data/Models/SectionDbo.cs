namespace TestTaskPravo.Data.Models;

/// <summary>
/// Класс для описания разделов
/// </summary>
public class SectionDbo
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = string.Empty; // max 1024

    /// <summary>
    /// Список тегов
    /// </summary>
    public List<SectionTagDbo> Tags { get; set; } = new();
}