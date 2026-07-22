namespace Stock.Domain;

public class Invoice : AggregateRoot
{
    public Guid OrderId { get; set; }
    public InvoiceCode InvoiceCode { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Tax { get; set; }
    public decimal FinalAmount { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
}
