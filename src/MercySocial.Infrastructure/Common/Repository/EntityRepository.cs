using MercySocial.Application.Common.Repository;
using MercySocial.Domain.common;
using MercySocial.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MercySocial.Infrastructure.Common.Repository;

public abstract class EntityRepository<TModel, TId, TIdType> :
    IRepository<TModel, TId, TIdType>
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

    public virtual Task<bool> ExistsBy(TModel entity) => Entities.ContainsAsync(entity);

    public virtual async Task<TModel?> ExistsBy(TId id) => await Entities.FindAsync(id.Value);

    public virtual async Task<TModel?> GetByIdAsync(TId id)
    {
        var entity = await Entities.FindAsync(id.Value);
        if (entity is null)
            throw new KeyNotFoundException();

        return entity;
    }

    public virtual async Task<TModel> AddAsync(TModel entity)
    {
        var addedEntity = await Entities.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public virtual Task UpdateAsync(TModel entity)
    {
        Entities.Update(entity);
        return DbContext.SaveChangesAsync();
    }

    public Task UpdateByIdAsync(TModel entity, TModel existingEntity)
    {
        DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        return DbContext.SaveChangesAsync();
    }

    public virtual Task DeleteAsync(TModel entity)
    {
        Entities.Remove(entity);
        return DbContext.SaveChangesAsync();
    }
}