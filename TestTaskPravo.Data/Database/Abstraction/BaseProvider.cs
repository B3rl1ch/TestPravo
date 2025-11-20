using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskPravo.Core.Database.Abstraction;
using TestTaskPravo.Core.Exceptions.Models;

namespace TestTaskPravo.Data.Database.Abstraction;

/// <summary>
/// Базовый провайдер для CRUD операций
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TDbo"></typeparam>
public abstract class BaseProvider<T, TDbo> : IBaseProvider<T>
    where T : class
    where TDbo : class
{
    public readonly AppDbContext DbContext;
    protected readonly IMapper Mapper;
    
    public DbContext AppDbContext { get; }

    protected BaseProvider(AppDbContext context, IMapper mapper)
    {
        DbContext = context;
        AppDbContext = context;
        Mapper = mapper;
    }

    public abstract Task UpdateAsync(T data, CancellationToken ct);

    public virtual async Task<T?> GetAsync(Guid key, CancellationToken ct)
    {
       var dbo = await DbContext.Set<TDbo>().FindAsync(new object[] { key }, ct);
        return dbo == null ? null : Mapper.Map<T>(dbo);
    }

    public virtual async Task<List<T>> ListAsync(CancellationToken ct)
    {
        var dbo = await DbContext.Set<TDbo>().ToListAsync(ct);
        return Mapper.Map<List<T>>(dbo);
    }


    public virtual async Task<T> AddAsync(T data, CancellationToken ct)
    {
        var dbo = Mapper.Map<TDbo>(data);
        DbContext.Add(dbo);

        return Mapper.Map<T>(dbo);
    }
    
    public virtual async Task DeleteAsync(Guid key, CancellationToken ct)
    {
        var dbo = await DbContext.Set<TDbo>().FindAsync(new object[] { key }, ct);
        if (dbo == null) return;

        DbContext.Remove(dbo);
    }
}
