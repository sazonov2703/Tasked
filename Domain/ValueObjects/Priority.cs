namespace Domain.ValueObjects;

public class Priority : BaseValueObject<Priority>
{
    private Priority(string value)
    {
        Value = value;
    }
    
    public string Value { get; private set; }
    
    public static Priority Low => new("Low");
    public static Priority Medium => new("Medium");
    public static Priority High => new("High");

    public static Priority FromString(string value)
    {
        return value switch
        {
            "Low" => Low,
            "Medium" => Medium,
            "High" => High,
            _ => throw new ArgumentException($"Invalid priority value: {value}")
        };
    }
}