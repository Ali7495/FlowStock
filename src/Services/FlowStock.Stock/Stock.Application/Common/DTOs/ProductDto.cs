namespace Stock.Application;

public record ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string Code { get; set; }
}
