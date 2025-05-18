using System.ComponentModel;
using MediatR;

namespace Domain.Interfaces;

/// <summary>
/// Базовый интерфейс для всех доменных событий.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Время возникновения события.
    /// </summary>
    DateTime OccurredAt { get; }
}