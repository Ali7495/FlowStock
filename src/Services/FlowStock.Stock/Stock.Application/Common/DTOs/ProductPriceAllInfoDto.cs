namespace Stock.Application;

public sealed class ProductPriceAllInfoDto
{
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public decimal Price { get; set; }
    public string IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeactivatedAt { get; set; }
}
