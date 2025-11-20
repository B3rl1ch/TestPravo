using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTaskPravo.Core.Database.Abstraction.Providers;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Data.Database;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Providers;

public class SectionProvider : ISectionProvider
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public SectionProvider(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<Section>> GetAllWithTagsAsync(CancellationToken ct)
    {
        var items = await _db.Sections
            .Include(s => s.Tags)
            .ThenInclude(st => st.Tag)
            .ToListAsync(ct);
        return _mapper.Map<List<Section>>(items);
    }

    public async Task<Section?> GetByIdWithTagsAsync(Guid id, CancellationToken ct)
    {
        var items = await _db.Sections
            .Include(s => s.Tags)
            .ThenInclude(st => st.Tag)
            .FirstOrDefaultAsync(s => s.Id == id, ct);
        
        return _mapper.Map<Section>(items);
    }

    public async Task AddAsync(Section section, CancellationToken ct)
    {
        var itemDbo = _mapper.Map<SectionDbo>(section);
        await _db.Sections.AddAsync(itemDbo, ct);
    }

    public async Task DeleteAsync(Section section, CancellationToken ct)
    {
        var dbo = await _db.Sections.FindAsync(new object[] { section.Id }, ct);
        if (dbo == null) return;
        _db.Sections.Remove(dbo);
    }
}