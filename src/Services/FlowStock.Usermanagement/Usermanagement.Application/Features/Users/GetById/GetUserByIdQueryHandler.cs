using AutoMapper;
using BuildingBlocks.Domain;
using MediatR;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.id,cancellationToken);

        if(user is null)
            throw new DomainExceptions("The user is not exist!");

        return _mapper.Map<UserDto>(user);    
    }
}
