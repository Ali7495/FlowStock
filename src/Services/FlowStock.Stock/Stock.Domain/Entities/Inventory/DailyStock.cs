namespace Stock.Domain;

public class DailyStock : BasicEntity
{
    public Guid ProductId { get; set; }
    public double CurrentStock { get; set; }



    public Product Product { get; set; }
}
