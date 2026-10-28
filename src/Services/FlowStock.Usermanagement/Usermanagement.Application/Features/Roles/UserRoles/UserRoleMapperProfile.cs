using AutoMapper;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public sealed class UserRoleMapperProfile : Profile
{
    public UserRoleMapperProfile()
    {
        CreateMap<UserRole, UserRoleDto>()
            .ForMember(opt => opt.RoleName, dest => dest.MapFrom(src => src.Role.Name))
            .ForMember(opt => opt.Username, dest => dest.MapFrom(src => src.User.Username));
    }
}
