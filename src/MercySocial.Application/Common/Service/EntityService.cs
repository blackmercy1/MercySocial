using ErrorOr;
using MercySocial.Application.Common.Repository;
using MercySocial.Domain.Common;
using MercySocial.Domain.Common.Errors;

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

    public async Task<ErrorOr<TModel>> DeleteByIdAsync(TId tId)
    {
        var existingEntity = await Repository.GetByIdAsync(tId);
        if (existingEntity is null)
            return Errors.Entity.EntityWasNotFound;

        await Repository.DeleteAsync(existingEntity);

        return existingEntity;
    }

    public virtual async Task<ErrorOr<TModel>> AddAsync(TModel entity)
    {
        var existingEntity = await Repository.ExistsBy(entity);
        if (existingEntity)
            return Errors.Entity.EntityExists;

        var addedEntity = await Repository.AddAsync(entity);

        return addedEntity;
    }

    public virtual async Task<ErrorOr<bool>> DeleteAsync(TId id)
    {
        var existingEntity = await Repository.GetByIdAsync(id);
        if (existingEntity is null)
            return Errors.Entity.EntityWasNotFound;

        await Repository.DeleteAsync(existingEntity);

        return true;
    }

    public async Task<ErrorOr<TModel>> UpdateByIdAsync(TModel entity, TId id)
    {
        var existingEntity = await Repository.GetByIdAsync(id);
        if (existingEntity is null)
            return Errors.Entity.EntityWasNotFound;
        
        await Repository.UpdateByIdAsync(entity, existingEntity);

        return existingEntity;
    }
}