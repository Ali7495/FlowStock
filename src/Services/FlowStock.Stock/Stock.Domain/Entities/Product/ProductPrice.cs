namespace Stock.Domain;

public class ProductPrice : BasicEntity
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public static ProductPrice Create(decimal price)
    {
        return new()
        {
            Price = price,
            IsActive = true
        };
    }

    public ICollection<OrderItem> OrderItems { get; set; }
}
