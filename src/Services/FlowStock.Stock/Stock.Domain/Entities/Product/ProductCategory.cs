namespace Stock.Domain;

public class ProductCategory : BasicEntity
{
    public ProductCategory()
    {
        Products = new List<Product>();
    }

    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }

    public static ProductCategory Create(string name)
    {
        return new()
        {
            Name = name
        };
    }
}
