namespace TestTaskPravo.Data.Models;

/// <summary>
/// Класс для описания тэгов разделов
/// </summary>
public class TagDbo
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty; 
}