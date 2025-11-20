namespace TestTaskPravo.Data.Models;

/// <summary>
/// Класс для описания тегов раздела
/// </summary>
public class SectionTagDbo
{
    /// <summary>
    /// ID раздела
    /// </summary>
    public Guid SectionId { get; set; }
    
    /// <summary>
    /// Раздел
    /// </summary>
    public SectionDbo Section { get; set; } = null!;

    
    /// <summary>
    /// ID тэга
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// Тэг
    /// </summary>
    public TagDbo Tag { get; set; } = null!;
}