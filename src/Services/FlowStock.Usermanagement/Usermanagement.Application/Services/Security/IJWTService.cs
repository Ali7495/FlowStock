using Usermanagement.Domain;

namespace Usermanagement.Application;

public interface IJWTService
{
    string GenerateAccessToken(User user);

    RefreshToken GenerateRefreshToken();
}
