namespace TestTaskPravo.Core.Models;

/// <summary>
/// Класс для описания тегов раздела
/// </summary>
public class SectionTag
{
    /// <summary>
    /// ID раздела
    /// </summary>
    public Guid SectionId { get; set; }
    
    /// <summary>
    /// Раздел
    /// </summary>
    public Section Section { get; set; } = null!;

    
    /// <summary>
    /// ID тэга
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// Тэг
    /// </summary>
    public Tag Tag { get; set; } = null!;
}