namespace Domain.Entities;

public class User : BaseEntity<User>
{
    public User(string username)
    {
        Username = username;
    }
    public string Username { get; protected set; }
    public List<UserTask> Tasks { get; protected set; }
}