namespace Stock.Domain;

public class InventoryTransaction : AggregateRoot
{
    public Guid ProductId { get; set; }
    public TransactionType TransactionType { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
    public Product Product { get; set; }


    public static InventoryTransaction Create(Guid productId, TransactionType transactionType, int quantity, decimal unitPrice, DateTime transactionDate, string? description)
    {
        return new()
        {
            ProductId = productId,
            TransactionType = transactionType,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TransactionDate = transactionDate,
            Description = description
        };
    }
}
