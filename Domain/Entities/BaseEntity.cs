namespace Domain.Entities;

public class BaseEntity<T>
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; protected init; }
}