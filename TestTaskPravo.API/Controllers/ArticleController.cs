using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTaskPravo.Core.Abstractions;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Model.Models;

namespace TestTaskPravo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public ArticleController(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить статью по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArticleDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ArticleDto>> Get(Guid id, CancellationToken ct)
    {
        var article = await _articleService.GetAsync(id, ct);
        return Ok(_mapper.Map<ArticleDto>(article));
    }

    /// <summary>
    /// Создать новую статью.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Article), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ArticleDto>> Create([FromBody] ArticleCreateDto data, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var created = await _articleService.AddAsync(data, ct);

        return CreatedAtAction(
            nameof(Get),
            new { id = created.Id },
            _mapper.Map<ArticleDto>(created));
    }

    /// <summary>
    /// Обновить существующую статью.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Article), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ArticleDto>> Update(Guid id, [FromBody] ArticleUpdateDto data, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        
        var updated = await _articleService.UpdateAsync(id, data, ct);
        return Ok(_mapper.Map<ArticleDto>(updated));
    }

    /// <summary>
    /// Удалить статью.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _articleService.DeleteAsync(id, ct);
        return NoContent();
    }
}
