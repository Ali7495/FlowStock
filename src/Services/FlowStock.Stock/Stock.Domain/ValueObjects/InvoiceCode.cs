using BuildingBlocks.Domain;

namespace Stock.Domain;

public record class InvoiceCode
{
    public string Value { get; set; }

    private InvoiceCode(string value)
    {
        Value = value;
    }

    public static InvoiceCode Create(string code)
    {
        if(string.IsNullOrWhiteSpace(code))
            throw new DomainExceptions("The code cannot be empty!");

        return new(code);    
    }
}
