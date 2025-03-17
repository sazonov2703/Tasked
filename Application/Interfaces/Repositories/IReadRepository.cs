using Microsoft.AspNetCore.OData.Query;

namespace Application.Interfaces.Repositories;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<T>> GetQuerybleAsync(ODataQueryOptions<T> oDataQueryOptions, CancellationToken cancellationToken);
}