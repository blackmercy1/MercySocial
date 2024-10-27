using MercySocial.Application.Common.Repository;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Application.Users.Repository;

public interface IUserRepository : IRepository<User, UserId, Guid>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}