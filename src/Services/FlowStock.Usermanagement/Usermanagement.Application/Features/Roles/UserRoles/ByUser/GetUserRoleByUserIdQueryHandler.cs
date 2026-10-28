using AutoMapper;
using BuildingBlocks.Application;
using MediatR;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public sealed class GetUserRoleByUserIdQueryHandler : IRequestHandler<GetUserRoleByUserIdQuery, UserRoleDto>
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IMapper _mapper;

    public GetUserRoleByUserIdQueryHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
    {
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
    }
    public async Task<UserRoleDto> Handle(GetUserRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        UserRole userRole = await _userRoleRepository.GetUserRoleWithParentsByUserIdAsync(request.userId, cancellationToken);

        if (userRole is null)
        {
            throw new NotFoundException(nameof(userRole),request.userId);
        }

        return _mapper.Map<UserRoleDto>(userRole);
    }
}
