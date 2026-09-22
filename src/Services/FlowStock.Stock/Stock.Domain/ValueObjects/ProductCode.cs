using BuildingBlocks.Domain;

public sealed record ProductCode
{
    public string Value { get; }

    public ProductCode(string value)
    {
        Value = value;
    }

    public static ProductCode CreateBySequence(long sequence)
    {
        if (sequence <= 0)
        {
            throw new DomainExceptions("Sequence must be greater than 0 !");
        }

        return new($"PRD-{sequence:D6}");
    }

    public static ProductCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainExceptions(
                "Product code cannot be empty.");

        return new ProductCode(value);
    }
}