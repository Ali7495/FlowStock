namespace Stock.Domain;

public class Product : BasicEntity
{
    public Product()
    {
        ProductPrices = new List<ProductPrice>();
    }

    public Guid ProductCategoryId { get; set; }
    public string Name { get; set; }

    public static Product Create(Guid productCategoryId, string name)
    {
        return new()
        {
            ProductCategoryId = productCategoryId,
            Name = name
        };
    }

    public ProductPrice CreateProductPrice(decimal price)
    {
        ProductPrice productPrice = ProductPrice.Create(price);

        AddPrice(productPrice);

        return productPrice;
    }

    private void AddPrice(ProductPrice productPrice)
    {
        ProductPrices.Add(productPrice);
    }

    public ICollection<ProductPrice> ProductPrices { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
}
