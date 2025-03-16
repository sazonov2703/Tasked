namespace Domain.Entities;

public class Task : BaseEntity<Task>
{
    string Name { get; set; }
    string Description { get; set; }
    
    Guid UserId { get; set; }
}