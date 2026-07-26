namespace Stock.Domain;

public class Order : AggregateRoot
{
    public Order()
    {
        OrderItems = new List<OrderItem>();
        Payments = new List<Payment>();
    }

    public Guid CustomerId { get; set; }
    public OrderCode Code { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Tax { get; set; }
    public string? Description { get; set; }


    public Customer Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<InventoryReservation> InventoryReservations { get; set; }
    public ICollection<Invoice> Invoices { get; set; }

    public static Order Create(Guid customerId, string code, OrderStatus status, DateTime orderDate, decimal totalAmount, decimal discount, decimal tax, string? description)
    {
        return new()
        {
            CustomerId = customerId,
            Code = OrderCode.Create(code),
            Status = status,
            OrderDate = orderDate,
            TotalAmount = totalAmount,
            DiscountAmount = discount,
            Tax = tax,
            Description = description
        };
    }

    public OrderItem CreateItem(Guid productPriceId, int quantity, decimal unitPrice, decimal discountAmount, decimal tax, string? description)
    {
        OrderItem orderItem = OrderItem.Create(productPriceId,quantity,unitPrice,discountAmount,tax,description);

        AddOrderItem(orderItem);

        return orderItem;
    }

    private void AddOrderItem(OrderItem orderItem)
    {
        OrderItems.Add(orderItem);
    }

}

