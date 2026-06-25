namespace Usermanagement.Domain;

public class User : BasicEntity
{
    public Guid PersonId { get; set; }
    public string Username { get; set; }
    public string LowerUsername {
        get
        {
            return Username != null ? Username.ToLower() : string.Empty;
        }
    }
    public string Mobile { get; set; }
    public Email Email { get; set; }
    public string HashedPassword { get; set; }



    public Person Person { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
