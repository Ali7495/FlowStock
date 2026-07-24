namespace Stock.Domain;

public class InventoryReservation : AggregateRoot
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public decimal Quantity { get; set; }
    public DateTime ReservedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }


    public Product Product { get; set; }
    public Order Order { get; set; }
}
