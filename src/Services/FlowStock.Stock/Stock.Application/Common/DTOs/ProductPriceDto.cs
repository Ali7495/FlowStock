namespace Stock.Application;

public sealed class ProductPriceDto
{
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
