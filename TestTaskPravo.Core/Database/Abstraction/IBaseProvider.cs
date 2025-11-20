using Microsoft.EntityFrameworkCore;

namespace TestTaskPravo.Core.Database.Abstraction;

public interface IBaseProvider<T> 
    where T : class
{
    
    /// <summary>
    /// Получение данных из БД по ID
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<T?> GetAsync(Guid key, CancellationToken ct);
    
    /// <summary>
    /// Получение всех данных из БД
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<List<T>> ListAsync(CancellationToken ct);
    
    /// <summary>
    /// Обновление записи
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task UpdateAsync(T data, CancellationToken ct);
    
    /// <summary>
    /// Добавить запись
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<T> AddAsync(T data, CancellationToken ct);
    
    /// <summary>
    /// Удаление записи из БД
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid key, CancellationToken ct);
}