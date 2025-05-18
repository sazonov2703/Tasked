using Domain.Interfaces;
using FluentValidation;

namespace Domain.Entities;

public class BaseEntity<T> where T : BaseEntity<T>
{
    /// <summary>
    /// Конструктор создания базовой сущности.
    /// </summary>
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
    
    /// <summary>
    /// Лист доменных событий.
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = [];
    
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; protected init; }
    
    #region Методы
    
    /// <summary>
    /// Выполняет валидацию сущности с использованием указанного валидатора.
    /// </summary>
    /// <param name="validator">Валидатор FluentValidator.</param>
    protected void ValidateEntity(AbstractValidator<T> validator)
    {
        var validationResult = validator.Validate((T)this);
        if (validationResult.IsValid)
        {
            return;
        }

        var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
        throw new ValidationException(errorMessages);
    }

    #region Системные методы

    /// <summary>
    /// Переопределение метода Equals для сравнения сущностей по идентификатору.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || obj.GetType() != GetType()) return false;

        return Id.Equals(((BaseEntity<T>)obj).Id);
    }

    /// <summary>
    /// Переопределение метода GetHashCode для получения хеш-кода на основе уникального идентификатора.
    /// </summary>
    /// <returns>Хеш-код, основанный на значении идентификатора.</returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Оператор равенства для сравнения двух экземпляров BaseEntity по идентификатору.
    /// </summary>
    /// <param name="left">Левая сущность для сравнения.</param>
    /// <param name="right">Правая сущность для сравнения.</param>
    /// <returns>True, если сущности равны; иначе False.</returns>
    public static bool operator ==(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// Оператор неравенства для сравнения двух экземпляров BaseEntity по идентификатору.
    /// </summary>
    /// <param name="left">Левая сущность для сравнения.</param>
    /// <param name="right">Правая сущность для сравнения.</param>
    /// <returns>True, если сущности не равны; иначе False.</returns>
    public static bool operator !=(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        return !(left == right);
    }

    #endregion
    
    #region Методы доменных событий

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    #endregion
    
    #endregion
}