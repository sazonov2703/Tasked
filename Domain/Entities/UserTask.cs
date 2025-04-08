using Domain.Events;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserTask : BaseEntity<UserTask>
{
    /// <summary>
    /// Пустой конструктор для EF core.
    /// </summary>
    private UserTask()
    {
        
    }
    /// <summary>
    /// Конструктор создания задачи.
    /// </summary>
    /// <param name="title">Название.</param>
    /// <param name="description">Описание.</param>
    /// <param name="status">Статус выполнения.</param>
    /// <param name="priority">Приоритет выполнения.</param>
    /// <param name="userId">Связь с пользователем(Id).</param>
    public UserTask(string title, string description, Status status, Priority priority, Guid userId)
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        UserId = userId;
        
        AddDomainEvent(new TodoTaskCreatedEvent(Id, Title, Description, Status, Priority));
    }

    #region Свойства
    
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Статус выполнения.
    /// </summary>
    public Status Status { get; private set; }
    
    /// <summary>
    /// Приоритет выполнения.
    /// </summary>
    public Priority Priority { get; private set; }
    
    /// <summary>
    /// Навигационные свойства.
    /// </summary>
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    
    #endregion
    
    #region Методы

    public void Update(string newTitle, string newDescription, Status newStatus, Priority newPriority)
    {
        string oldTitle = Title;
        string oldDescription = Description;
        Status oldStatus = Status;
        Priority oldPriority = Priority;
        
        Title = newTitle;
        Description = newDescription;
        Status = newStatus;
        Priority = newPriority;
        
        AddDomainEvent(new TodoTaskUpdatedEvent(
            Id, newTitle, newDescription, newStatus, newPriority, 
            oldTitle, oldDescription, oldStatus, oldPriority));
    }
    
    #endregion
}