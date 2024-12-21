using MercySocial.Application.Common.Repository;
using MercySocial.Domain.Common;
using MercySocial.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MercySocial.Infrastructure.Common.Repository;

public abstract class EntityRepository<TModel, TId, TIdType> :
    IRepository<TModel, TId>
    where TModel : Entity<TId>
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    protected readonly ApplicationDbContext DbContext;
    protected DbSet<TModel> Entities => DbContext.Set<TModel>();

    protected EntityRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual Task<bool> ExistsByAsync(
        TModel entity,
        CancellationToken cancellationToken)
        => Entities.ContainsAsync(entity, cancellationToken: cancellationToken);

    public virtual async Task<bool> ExistsByIdAsync(
        TId id,
        CancellationToken cancellationToken) 
        => await Entities.AnyAsync(e => e.Id == id, cancellationToken);

    public virtual async Task<TModel?> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken) 
        => await Entities.FindAsync([id, cancellationToken], cancellationToken);

    public virtual async Task<TModel> AddAsync(
        TModel entity,
        CancellationToken cancellationToken)
    {
        var addedEntity = await Entities.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return addedEntity.Entity;
    }

    public Task UpdateByIdAsync(
        TModel entity,
        TModel existingEntity,
        CancellationToken cancellationToken)
    {
        DbContext
            .Entry(existingEntity).CurrentValues
            .SetValues(entity);
        
        return DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual Task DeleteAsync(
        TModel entity,
        CancellationToken cancellationToken)
    {
        Entities.Remove(entity);
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}