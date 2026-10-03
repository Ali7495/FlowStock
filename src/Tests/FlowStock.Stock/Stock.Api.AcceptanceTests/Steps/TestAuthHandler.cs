using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Stock.Api.AcceptanceTests;

public sealed class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "Test";

    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory loggerFactory, UrlEncoder urlEncoder)
    : base(options, loggerFactory, urlEncoder)
    {

    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Test-UserId", out var userId))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        List<Claim> claims = [
            new("sub", userId.ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString())
        ];

        if (Request.Headers.TryGetValue("X-Test-Permission", out var permission))
        {
            claims.Add(new("permission", permission.ToString()));
        }

        ClaimsIdentity identity = new(claims, SchemeName);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket authenticationTicket = new(principal, SchemeName);

        return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
    }
}
