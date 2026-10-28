using System.Collections.Specialized;
using System.ComponentModel;
using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJWTService _jWTService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRolePermissionRepository rolePermissionRepository, IPasswordService passwordService,
     IJWTService jWTService, IUnitOfWork unitOfWork, ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _passwordService = passwordService;
        _jWTService = jWTService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Some user with the username: {Username} is trying to login.", request.username);
        
        User user = await _userRepository.GetLoginByNormalizedUsernameAsync(request.username, cancellationToken);
        if (user is null)
            throw new DomainExceptions("The username or password is not valid!");

        if (!_passwordService.Verify(request.password, user.HashedPassword))
            throw new DomainExceptions("The username or password is not valid!");

        List<string> permissionNames = await GetUserPermissionsNames(user.Id,cancellationToken);    

        string accessToken = _jWTService.GenerateAccessToken(user, permissionNames);

        RefreshToken refreshToken = _jWTService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The user with username {Username} loged in successfully. ", request.username);

        return new(accessToken, refreshToken.Token, refreshToken.ExpiredAt);

    }

    private async Task<List<string>> GetUserPermissionsNames(Guid userId, CancellationToken cancellationToken)
    {
        UserRole userRole = await _userRoleRepository.GetUserRoleWithParentsByUserIdAsync(userId, cancellationToken);

        if(userRole is null)
            throw new NotFoundException(nameof(userRole), userId);

        List<RolePermission> rolePermissions = await _rolePermissionRepository.GetAllPermissionsWithRoleByRoleIdAsync(userRole.RoleId, cancellationToken);

        return rolePermissions.Select(r=> r.Permission.Name).ToList();
    }
}
