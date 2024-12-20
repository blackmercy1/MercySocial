using MercySocial.Application.Users.Repository;
using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;
using MercySocial.Infrastructure.Common.Repository;
using MercySocial.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MercySocial.Infrastructure.Users.Repository;

public class UserRepository :
    EntityRepository<User, UserId, Guid>,
    IUserRepository
{
    public UserRepository(
        ApplicationDbContext dbContext) : base(
        dbContext)
    {
    }

    public Task<UserId?> GetIdByEmailAsync(
        string email,
        CancellationToken cancellationToken)
        => DbContext
            .Set<User>()
            .Where(x => x.Email == email)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);


    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        DbContext
            .Set<User>()
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken)
        => DbContext
            .Set<User>()
            .AnyAsync(x => x.Email == email, cancellationToken);
}