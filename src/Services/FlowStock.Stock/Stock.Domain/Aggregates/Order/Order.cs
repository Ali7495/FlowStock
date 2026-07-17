namespace Stock.Domain;

public class Order : AggregateRoot
{
    public Guid CustomerId { get; set; }
    public OrderCode Code { get; set; }
    
}
