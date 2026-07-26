namespace Stock.Domain;

public class Invoice : AggregateRoot
{

    public Invoice()
    {
        InvoiceItems = new List<InvoiceItem>();
    }

    public Guid OrderId { get; set; }
    public InvoiceCode InvoiceCode { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Tax { get; set; }
    public decimal FinalAmount { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public Order Order { get; set; }



    public static Invoice Create(Guid orderId, string code, InvoiceStatus invoiceStatus, DateTime issueDate, decimal totalAmount, decimal discount, decimal tax, decimal finalAmount)
    {
        return new()
        {
            OrderId = orderId,
            InvoiceCode = InvoiceCode.Create(code),
            InvoiceStatus = invoiceStatus,
            IssueDate = issueDate,
            TotalAmount = totalAmount,
            DiscountAmount = discount,
            Tax = tax,
            FinalAmount = finalAmount
        };
    }

    public InvoiceItem CreateItem(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount, decimal tax)
    {
        InvoiceItem invoiceItem = InvoiceItem.Create(productId,productName,quantity,unitPrice,discount,tax);

        AddInvoiceItem(invoiceItem);

        return invoiceItem;
    }


    private void AddInvoiceItem(InvoiceItem invoiceItem)
    {
        InvoiceItems.Add(invoiceItem);
    }
}
