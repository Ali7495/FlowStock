using MediatR;

namespace Usermanagement.Application;

public record LoginCommand(string username, string password) : IRequest<LoginResponse>;