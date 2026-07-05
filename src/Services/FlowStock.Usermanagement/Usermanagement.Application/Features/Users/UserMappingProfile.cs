using AutoMapper;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
       CreateMap<User, UserDto>()
            .ForCtorParam(nameof(UserDto.FirstName),
                opt => opt.MapFrom(src => src.Person.FirstName))
            .ForCtorParam(nameof(UserDto.LastName),
                opt => opt.MapFrom(src => src.Person.LastName))
            .ForCtorParam(nameof(UserDto.Email),
                opt => opt.MapFrom(src => src.Email.Value));
        
    }
}
