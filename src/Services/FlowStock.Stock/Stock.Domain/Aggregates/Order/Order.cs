namespace Stock.Domain;

public class Order : AggregateRoot
{
    public Guid CustomerId { get; set; }
    public OrderCode Code { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Tax { get; set; }
    public string? Description { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<InventoryReservation> InventoryReservations { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
    
}
