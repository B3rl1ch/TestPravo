namespace TestTaskPravo.Model.Models;

public class SectionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public List<ArticleDto> Articles { get; set; } = new();
}