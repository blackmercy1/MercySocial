using MercySocial.Domain.Common;

namespace MercySocial.Application.Common.Repository;

public interface IRepository<TModel, in TId, in TIdType>
    where TModel : Entity<TId> 
    where TId : ValueObject
    where TIdType : struct
{
    Task<TModel?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<bool> ExistsBy(TModel entity, CancellationToken cancellationToken);
    
    Task<TModel> AddAsync(TModel entity, CancellationToken cancellationToken);
    Task UpdateByIdAsync(TModel entity, TModel existingEntity, CancellationToken cancellationToken);
    
    Task DeleteAsync(TModel entity, CancellationToken cancellationToken);
}