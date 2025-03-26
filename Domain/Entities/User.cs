using System.ComponentModel.DataAnnotations;
using Domain.Events;

namespace Domain.Entities;

public class User : BaseEntity<User>
{
    /// <summary>
    /// Конструктор создания пользователя.
    /// </summary>
    /// <param name="username">Юзернейм.</param>
    /// <param name="email">Электронная почта.</param>
    /// <param name="passwordHash">Хэш пароля.</param>
    public User(string username, string email, string passwordHash)
    {
        Username = username;
        Email = Email;
        PasswordHash = passwordHash;
        
       AddDomainEvent(new UserCreatedEvent(Id, Username, Email, PasswordHash)); 
    }
    
    #region Свойства
    
    /// <summary>
    /// Юзернейм.
    /// </summary>
    public string Username { get; protected set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; protected set; }
    
    /// <summary>
    /// Хэш пароля.
    /// </summary>
    public string PasswordHash { get; protected set; }
    
    /// <summary>
    /// Навигационные свойства.
    /// </summary>
    public List<TodoTask> Tasks { get; protected set; }
    
    #endregion
    
    #region Методы

    public void Update(string newUsername, string newEmail, string newPasswordHash)
    {
        string oldUsername = Username;
        string oldEmail = Email;
        string oldPasswordHash = PasswordHash;
        Username = newUsername;
        Email = newEmail;
        PasswordHash = newPasswordHash;
        
        AddDomainEvent(new UserUpdatedEvent(
            Id, newUsername, newEmail, newPasswordHash,
            oldUsername, oldEmail, oldPasswordHash));
    }
    
    #endregion
}