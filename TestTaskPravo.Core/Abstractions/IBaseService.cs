namespace TestTaskPravo.Core.Abstractions;

public interface IBaseService<T, TCreate, TUpdate> 
    where T: class
    where TCreate: class
    where TUpdate: class
{
    /// <summary>
    /// Get data from database
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<T?> GetAsync(Guid key, CancellationToken ct);
    
    /// <summary>
    /// Get all data from database
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<List<T>> ListAsync(CancellationToken ct);
    
    /// <summary>
    /// Save data to database
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<T> AddAsync(TCreate data, CancellationToken ct);

    /// <summary>
    /// Update data in database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<T?> UpdateAsync(Guid id, TUpdate data, CancellationToken ct);
    
    /// <summary>
    /// Delete data from database
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid key, CancellationToken ct);
}