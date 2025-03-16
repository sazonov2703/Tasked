namespace Domain.Entities;

public class User : BaseEntity<User>
{
    public string Username { get; set; }
    
    List<Task> Tasks { get; set; } = new List<Task>();
}