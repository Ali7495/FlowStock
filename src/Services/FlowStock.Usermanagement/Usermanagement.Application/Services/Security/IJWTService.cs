using Usermanagement.Domain;

namespace Usermanagement.Application;

public interface IJWTService
{
    string GenerateToken(User user);
}
