namespace Application.Interfaces.Repository;

public interface IWriteRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}