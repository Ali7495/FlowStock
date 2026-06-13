namespace Usermanagement.Domain;

public class User : BasicProperties
{
    public Guid PersonId { get; set; }
    public string Username { get; set; }
    public string LowerUsername { get; set; }
    public string HashedPassword { get; set; }



    public Person Person { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
