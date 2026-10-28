using Usermanagement.Domain;

namespace Usermanagement.Application;

public interface IJWTService
{
    string GenerateAccessToken(User user, List<string> permissions);

    RefreshToken GenerateRefreshToken();
}
