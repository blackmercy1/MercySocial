using MercySocial.Application.Common.Repository;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Application.Common.Users.Repository;

public interface IUserRepository : IRepository<User, UserId, int>;