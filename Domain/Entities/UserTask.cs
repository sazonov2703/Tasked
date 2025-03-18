namespace Domain.Entities;

public class UserTask : BaseEntity<UserTask>
{
    public UserTask(string name, string description, Guid userId)
    {
        Name = name;
        Description = description;
        UserId = userId;
    }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    
    public Guid UserId { get; protected set; }
}