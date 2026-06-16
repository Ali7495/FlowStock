namespace Usermanagement.Domain;

public class User : BasicEntity
{
    public Guid PersonId { get; set; }
    public string Username { get; set; }
    public string Mobile { get; set; }
    public Email email { get; set; }
    public string HashedPassword { get; set; }



    public Person Person { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
