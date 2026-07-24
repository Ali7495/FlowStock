namespace Stock.Domain;

public class InvoiceItem : BasicEntity
{
    public Guid ProductId { get; set; }
    public Guid InvoiceId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Tax { get; set; }

    public Product Product { get; set; }
    public Invoice Invoice { get; set; }
}
