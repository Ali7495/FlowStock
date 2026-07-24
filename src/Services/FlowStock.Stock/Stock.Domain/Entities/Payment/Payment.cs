namespace Stock.Domain;

public class Payment : AggregateRoot
{
    public Guid OrderId { get; set; }
    public PaymentCode PaymentCode { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? TransactionId { get; set; }
    public string? ReferenceName { get; set; }
    public string? Description { get; set; }

    public Order Order { get; set; }
}
