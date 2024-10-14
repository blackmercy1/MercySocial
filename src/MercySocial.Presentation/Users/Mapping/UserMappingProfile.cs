using AutoMapper;
using MercySocial.Domain.UserAggregate;
using MercySocial.Presentation.Users.Dto;

namespace MercySocial.Presentation.Users.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserDto, User>();
    }
}