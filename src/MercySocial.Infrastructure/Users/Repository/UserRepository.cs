using MercySocial.Application.Common.Users.Repository;
using MercySocial.Application.Common.Users.Service;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;
using MercySocial.Infrastructure.Common.Repository;
using MercySocial.Infrastructure.Data;

namespace MercySocial.Infrastructure.Users.Repository;

public class UserRepository : EntityRepository<User, UserId, int>, IUserRepository
{
    public UserRepository(
        ApplicationDbContext dbContext) : base(
        dbContext)
    {
    }
}