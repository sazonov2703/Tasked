namespace Application.Exceptions;

public class TaskedException : Exception
{
    public TaskedException(string message) : base(message)
    {
    }

    public TaskedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class TaskNotFoundException : TaskedException
{
    public TaskNotFoundException(Guid taskId) 
        : base($"Task with ID {taskId} not found.")
    {
    }
}

public class UserNotFoundException : TaskedException
{
    public UserNotFoundException(Guid userId) 
        : base($"User with ID {userId} not found.")
    {
    }

    public UserNotFoundException(string email) 
        : base($"User with email {email} not found.")
    {
    }
}

public class UnauthorizedTaskAccessException : TaskedException
{
    public UnauthorizedTaskAccessException(Guid userId, Guid taskId) 
        : base($"User {userId} is not authorized to access task {taskId}")
    {
    }
}

public class DuplicateEmailException : TaskedException
{
    public DuplicateEmailException(string email) 
        : base($"User with email {email} already exists.")
    {
    }
} 