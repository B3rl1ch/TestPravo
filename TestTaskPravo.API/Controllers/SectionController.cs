using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTaskPravo.Core.Abstractions;
using TestTaskPravo.Model.Models;

namespace TestTaskPravo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;
    private readonly IMapper _mapper;

    public SectionController(ISectionService sectionService, IMapper mapper)
    {
        _sectionService = sectionService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить список разделов, отсортированный по убыванию количества статей.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<SectionDto>), 200)]
    public async Task<ActionResult<List<SectionDto>>> GetSections(CancellationToken ct)
    {
        var sections = await _sectionService.GetSectionsAsync(ct);
        return Ok(_mapper.Map<List<SectionDto>>(sections));
    }

    /// <summary>
    /// Получить статьи для раздела.
    /// </summary>
    [HttpGet("{id:guid}/articles")]
    [ProducesResponseType(typeof(List<ArticleDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<ArticleDto>>> GetSectionArticles(Guid id, CancellationToken ct)
    {
        var articles = await _sectionService.GetSectionArticlesAsync(id, ct);
        return Ok(_mapper.Map<List<ArticleDto>>(articles));
    }
}