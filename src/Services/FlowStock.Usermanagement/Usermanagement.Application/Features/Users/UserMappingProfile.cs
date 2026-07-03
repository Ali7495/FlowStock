using AutoMapper;
using Usermanagement.Domain;

namespace Usermanagement.Application;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User,UserDto>()
        .ForMember(u=> u.FirstName,u=> u.MapFrom(x=> x.Person.FirstName));

        CreateMap<User,UserDto>()
        .ForCtorParam(nameof(UserDto.LastName),u=> u.MapFrom(x=> x.Person.LastName));
        
        CreateMap<User,UserDto>()
        .ForCtorParam(nameof(UserDto.Email),u=> u.MapFrom(x=> x.Email.Value));
        ;
    }
}
