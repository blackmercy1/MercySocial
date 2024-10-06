using MercySocial.Application.Common.Service;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Application.Common.Users.Service;

public interface IUserService : IService<User, UserId, int>;