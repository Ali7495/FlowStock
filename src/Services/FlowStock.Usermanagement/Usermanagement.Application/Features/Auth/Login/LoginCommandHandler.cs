using BuildingBlocks.Domain;
using MediatR;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJWTService _jWTService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordService passwordService, IJWTService jWTService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jWTService = jWTService;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetLoginByNormalizedUsernameAsync(request.username, cancellationToken);
        if (user is null)
            throw new DomainExceptions("The username or password is not valid!");

        if (!_passwordService.Verify(request.password, user.HashedPassword))
            throw new DomainExceptions("The username or password is not valid!");

        string accessToken = _jWTService.GenerateAccessToken(user);

        RefreshToken refreshToken = _jWTService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new(accessToken, refreshToken.Token, refreshToken.ExpiredAt);

    }
}
