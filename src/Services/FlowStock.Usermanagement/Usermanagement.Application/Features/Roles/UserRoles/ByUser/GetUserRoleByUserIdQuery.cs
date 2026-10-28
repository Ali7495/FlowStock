using MediatR;

namespace Usermanagement.Application;

public sealed record GetUserRoleByUserIdQuery(Guid userId) : IRequest<UserRoleDto>;