using System.ComponentModel.DataAnnotations;

namespace TestTaskPravo.Core.Models;

/// <summary>
/// Класс для описания тэгов разделов
/// </summary>
public class Tag
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