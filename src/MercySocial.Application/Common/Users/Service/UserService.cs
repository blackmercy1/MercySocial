using MercySocial.Application.Common.Service;
using MercySocial.Application.Common.Users.Repository;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Application.Common.Users.Service;

public class UserService : EntityService<User, UserId, int>, IUserService
{
    public UserService(IUserRepository repository) : base(
        repository)
    {
    }
}