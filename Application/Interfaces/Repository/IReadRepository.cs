using Microsoft.AspNetCore.OData.Query;

namespace Application.Interfaces.Repository;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<T>> GetQuerybleAsync(ODataQueryOptions<T> oDataQueryOptions, CancellationToken cancellationToken);
}