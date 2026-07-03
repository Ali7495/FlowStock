namespace Usermanagement.Domain;

public class User : BasicEntity
{
    public Guid PersonId { get; set; }
    public string Username { get; set; }
    public string NormalizedUsername { get; set; }
    public string Mobile { get; set; }
    public Email Email { get; set; }
    public string HashedPassword { get; set; }

    public static User Create(string username, Email email, string hashedPassword, string mobile)
    {
        return new()
        {
            Username = username,
            NormalizedUsername = username.Trim().ToLower(),
            HashedPassword = hashedPassword,
            Email = email,
            Mobile = mobile
        };
    }

    public Person Person { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
