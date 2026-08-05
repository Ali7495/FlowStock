using BuildingBlocks.Domain;

namespace Stock.Domain;

public record class PaymentCode
{
    public string Value { get; set; }

    private PaymentCode(string value)
    {
        Value = value;
    }

    public static PaymentCode Create(string code)
    {
        if(string.IsNullOrWhiteSpace(code))
            throw new DomainExceptions("Payment code can not be empty!");

        return new(code);    
    }
}
