using BuildingBlocks.Domain;

public sealed record ProductCode
{
    public string Value { get; set; }

    public ProductCode(string value)
    {
        Value = value;
    }

    public static ProductCode Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new DomainExceptions("The code can not be null!");

        return new(code);    
    }
}