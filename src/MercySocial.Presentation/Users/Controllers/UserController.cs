using AutoMapper;
using MercySocial.Application.Common.Users;
using MercySocial.Application.Common.Users.Service;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;
using MercySocial.Presentation.Common.Controllers;
using MercySocial.Presentation.Users.Dto;

namespace MercySocial.Presentation.Users.Controllers;

public class UserController : EntityController<User, UserDto, UserId, int>
{
    public UserController(
        IUserService service,
        IMapper mapper) : base(
            service,
            mapper)
    { }
}
