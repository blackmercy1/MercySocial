using AutoMapper;
using MercySocial.Application.Common.Authentication;
using MercySocial.Application.Users.Commands.CreateRegisterUser;
using MercySocial.Application.Users.Commands.CreateUser;
using MercySocial.Application.Users.Queries.UserLogin.Queries;
using MercySocial.Domain.UserAggregate;
using MercySocial.Presentation.Common.Authentication;
using MercySocial.Presentation.Users.Requests;
using MercySocial.Presentation.Users.Responses;

namespace MercySocial.Presentation.Users.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<User, RegisterResponse>();
        CreateMap<UserLoginRequest, UserLoginQuery>();
        CreateMap<CreateUserRegisterRequest, CreateUserRegisterCommand>();

        MapAuthenticationResult();
    }

    private void MapAuthenticationResult()
    {
        CreateMap<AuthenticationResult, AuthenticationResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));
    }
}