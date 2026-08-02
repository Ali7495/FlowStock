using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Usermanagement.Application;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class JwtService : IJWTService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateAccessToken(User user)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.SecretKey));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        IEnumerable<Claim> claims = CreateClaims(user);

        JwtSecurityToken token = new(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpirationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[64];

        RandomNumberGenerator.Fill(bytes);

        return RefreshToken.Create(Convert.ToBase64String(bytes), DateTime.UtcNow.AddDays(30));
    }


    private IEnumerable<Claim> CreateClaims(User user)
    {
        List<Claim> claims = new();

        claims.Add(new (JwtRegisteredClaimNames.Sub, user.PersonId.ToString()));
        claims.Add(new ("userId",user.Id.ToString()));
        claims.Add(new (JwtRegisteredClaimNames.UniqueName, user.Username));
        claims.Add(new (JwtRegisteredClaimNames.Email, user.Email.Value));
        claims.Add(new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new ("permission", Permissions.ProductCategoryCreate));

        return claims;
    }
}
