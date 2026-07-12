namespace Usermanagement.Application;

public record LoginResponse(string accessToken, string refreshToken, DateTime expiresAt);