using MercySocial.Domain.Common;

namespace MercySocial.Application.Common.Repository;

public interface IRepository<TModel, in TId>
    where TModel : Entity<TId> 
    where TId : ValueObject
{
    Task<TModel?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<bool> ExistsByAsync(TModel entity, CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(TId entity, CancellationToken cancellationToken);
    
    Task<TModel> AddAsync(TModel entity, CancellationToken cancellationToken);
    Task UpdateByIdAsync(TModel entity, TModel existingEntity, CancellationToken cancellationToken);
    
    Task DeleteAsync(TModel entity, CancellationToken cancellationToken);
    
}