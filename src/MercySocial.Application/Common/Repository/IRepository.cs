using MercySocial.Domain.common;

namespace MercySocial.Application.Common.Repository;

public interface IRepository<TModel, in TId, in TIdType>
    where TModel : Entity<TId> 
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    Task<TModel?> GetByIdAsync(TId id);
    
    Task<TModel> AddAsync(TModel entity);
    Task UpdateByIdAsync(TModel entity, TModel existingEntity);
    Task UpdateAsync(TModel entity);
    
    Task DeleteAsync(TModel entity);
    Task<bool> ExistsBy(TModel entity);
    Task<TModel?> ExistsBy(TId id);
}