namespace Stock.Domain;

public class Customer : AggregateRoot
{
    public Guid PersonId { get; set; }
    public CustomerLevel CustomerLevel { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentDepth { get; set; }
    public bool IsBlackListed { get; set; }
    public string? InternalNote { get; set; }

    public ICollection<Order> Orders { get; set; }
}
