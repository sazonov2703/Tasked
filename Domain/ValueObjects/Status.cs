namespace Domain.ValueObjects;

public class Status : BaseValueObject<Status>
{
    private Status(string value)
    {
        Value = value;
    }
    
    public string Value { get; private set; }
    
    public static Status Todo => new("Todo");
    public static Status InProgress => new("InProgress");
    public static Status Done => new("Done");

    public static Status FromString(string value)
    {
        return value switch
        {
            "Todo" => Todo,
            "InProgress" => InProgress,
            "Done" => Done,
            _ => throw new ArgumentException($"Invalid status value: {value}")
        };
    }
}