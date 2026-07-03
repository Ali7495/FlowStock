namespace Usermanagement.Domain;

public class RefreshToken : BasicEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiredAt;
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsActive => !IsRevoked && !IsExpired;


    public User User { get; set; }
}
