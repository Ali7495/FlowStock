using MediatR;

namespace Usermanagement.Application;

public sealed record GetUserByIdQuery(Guid id) : IRequest<UserDto>;
