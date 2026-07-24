namespace Stock.Domain;

public class OrderItem : BasicEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductPriceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Description { get; set; }

    public Order Order { get; set; }
    public ProductPrice ProductPrice { get; set; }
}
