using ErrorOr;
using MercySocial.Application.Common.Repository;
using MercySocial.Domain.common;
using MercySocial.Domain.common.Errors.Entity;

namespace MercySocial.Application.Common.Service;

//Base class for all CRUD's
public abstract class EntityService<TModel, TId, TIdType> : 
    IService<TModel, TId, TIdType>
    where TModel : Entity<TId> 
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    protected readonly IRepository<TModel, TId, TIdType> Repository;

    protected EntityService(IRepository<TModel, TId, TIdType> repository)
    {
        Repository = repository;
    }
    
    public virtual async Task<ErrorOr<TModel>> GetByIdAsync(TId id)
    {
        var entity = await Repository.GetByIdAsync(id);
        return entity is null 
            ? Errors.Entity.EntityWasNotFound
            : entity;
    }
    
    public virtual async Task<ErrorOr<TModel>> AddAsync(TModel entity)
    {
        var existingEntity = await Repository.ExistsBy(entity);
        if (existingEntity)
            return Errors.Entity.EntityExists;

        var addedEntity = await Repository.AddAsync(entity);

        return addedEntity;
    }

    public virtual async Task<ErrorOr<TModel>> DeleteAsync(TModel entity)
    {
        var existingEntity = await Repository.ExistsBy(entity);
        if (!existingEntity)
            return Errors.Entity.EntityWasNotFound;

        await Repository.DeleteAsync(entity);

        return Result.Success;
    }

    public async Task<ErrorOr<TModel>> UpdateByIdAsync(TModel entity, TId id)
    {
        var existingEntity = await Repository.ExistsBy(id);
        if (existingEntity is null)
            return Result.Fail<TModel>("DoesNotExists");
        
        await Repository.UpdateByIdAsync(entity, existingEntity);

        return Result.Ok();
    }

    public virtual async Task<ErrorOr<TModel>> UpdateAsync(TModel entity)
    {
        var existingEntity = await Repository.ExistsBy(entity.Id);
        if (existingEntity is null)
            return Result.Fail<TModel>("DoesNotExists");
        
        await Repository.UpdateByIdAsync(entity, existingEntity);

        return Result.Ok();
    }
}