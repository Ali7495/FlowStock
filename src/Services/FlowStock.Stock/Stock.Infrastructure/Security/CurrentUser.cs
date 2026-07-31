using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Stock.Application;

namespace Stock.Infrastructure;

public sealed class CurrentUser : ICurrentUser
{

    private readonly IHttpContextAccessor _context;

    public CurrentUser(IHttpContextAccessor context)
    {
        _context = context;
    }

    private ClaimsPrincipal User => _context.HttpContext?.User!;
    
    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

    public Guid PersonId => Guid.Parse(User.FindFirst("sub")!.Value);

    public Guid UserId => Guid.Parse(User.FindFirst("userId")!.Value);

    public string Username => User.Identity?.Name ?? string.Empty;

    public IReadOnlyCollection<string> Roles => User.FindAll(ClaimTypes.Role).Select(r=> r.Value).ToList();

    public bool IsInRole(string role)
    {
        return Roles.Contains(role);
    }
}
