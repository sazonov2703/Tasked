namespace Domain.ValueObjects;

public class Priority : BaseValueObject<Priority>
{
    public string Value { get; private set; }
}