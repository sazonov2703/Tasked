namespace Domain.Entities;

public class BaseEntity<T>
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
    Guid Id { get; set; }
}