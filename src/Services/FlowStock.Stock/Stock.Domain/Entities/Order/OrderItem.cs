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

    public static OrderItem Create(Guid productPriceId, int quantity, decimal unitPrice, decimal discountAmount, decimal tax, string? description)
    {
        return new()
        {
            ProductPriceId = productPriceId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            DiscountAmount = discountAmount,
            Tax = tax,
            TotalAmount = (unitPrice * quantity) - discountAmount + tax,
            Description = description
        };
    }

    
}
