using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Write;

public abstract class BaseWriteRepository<T>(DbContext context) : IWriteRepository<T> where T : class
{
    protected readonly DbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Task.Run(() => _dbSet.Add(entity), cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await Task.Run(() => _dbSet.Update(entity), cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    { 
        var entity = await _dbSet.FindAsync([id], cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Сущность {typeof(T).Name} с Id {id} не найдена.");
        }
        
        await Task.Run(() => _dbSet.Remove(entity), cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}