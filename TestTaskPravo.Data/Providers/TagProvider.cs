using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Data.Database;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Providers;

public class TagProvider : ITagProvider
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public TagProvider(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Tag> AddAsync(string name, CancellationToken ct)
    {
        var itemDbo = new TagDbo()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        
        await _dbContext.Tags.AddAsync(itemDbo, ct);
        return _mapper.Map<Tag>(itemDbo);
    }

    public async Task<Tag?> GetByNameAsync(string normalizedName, CancellationToken ct)
    {
        var dbo = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Name.ToLower() == normalizedName, ct);
        return dbo == null ? null : _mapper.Map<Tag>(dbo);
    }

    public async Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names, CancellationToken ct)
    {
        var normalized = names
            .Select(n => n.Trim().ToLower())
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        if (normalized.Count == 0)
            return new List<Tag>();

        var items = await _dbContext.Tags
            .Where(t => normalized.Contains(t.Name.ToLower()))
            .ToListAsync(ct);
        
        return _mapper.Map<List<Tag>>(items);
    }
}