using System.ComponentModel.DataAnnotations;
using TestTaskPravo.Model.Validation;

namespace TestTaskPravo.Model.Models;

public class ArticleDto
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Title { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(256)]
    [MaxTagLength(256)]
    public List<string> Tags { get; set; }
}

public class ArticleCreateDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(256)]
    [MaxTagLength(256)]
    public List<string> Tags { get; set; } = new();
}

public class ArticleUpdateDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(256)]
    [MaxTagLength(256)]
    public List<string> Tags { get; set; } = new();
}