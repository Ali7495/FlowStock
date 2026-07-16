namespace Stock.Domain;

public record class OrderCode
{
    public string Value { get; set; }

    private OrderCode(string value)
    {
        Value = value;
    }

    public static OrderCode Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new DomainExceptions("the code can not be null or emtpy!");

        return new(code);
    }
}
