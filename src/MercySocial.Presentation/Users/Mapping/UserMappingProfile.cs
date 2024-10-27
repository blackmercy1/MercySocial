using AutoMapper;
using MercySocial.Application.Users.Commands.CreateUser;
using MercySocial.Domain.UserAggregate;
using MercySocial.Presentation.Users.Requests;
using MercySocial.Presentation.Users.Responses;

namespace MercySocial.Presentation.Users.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<User, UserResponse>();
    }
}