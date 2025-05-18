using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public abstract class BaseReadRepository<T>(DbContext context) : IReadRepository<T> where T : class
{
    protected readonly DbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync([id], cancellationToken);
        
        if (entity == null)
        {
            throw new KeyNotFoundException($"Сущность {typeof(T).Name} с Id {id} не найдена.");
        }
        
        return entity;
    }

    public Task<IQueryable<T>> GetQueryableAsync(ODataQueryOptions<T> oDataQueryOptions, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }
}