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
    { }

    public Task<UserId?> GetIdByEmailAsync(
        string email,
        CancellationToken cancellationToken) 
        => DbContext
            .Set<User>()
            .Where(x => x.Email == email)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<User> AddAsync(
        User entity,
        CancellationToken cancellationToken)
    {
        var addedEntity = await Entities.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return addedEntity.Entity;
    }
}