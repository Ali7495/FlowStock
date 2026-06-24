namespace Usermanagement.Domain;

public abstract class BasicEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
