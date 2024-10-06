using MercySocial.Domain.common;
using ErrorOr;

namespace MercySocial.Application.Common.Service;

public interface IService<TModel, in TId, in TIdType>
    where TModel : Entity<TId> 
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    Task<ErrorOr<TModel>> GetByIdAsync(TId id);
    Task<ErrorOr<TModel>> AddAsync(TModel entity);
    Task<ErrorOr<TModel>> DeleteAsync(TModel entity);
    Task<ErrorOr<TModel>> UpdateAsync(TModel entity);
    Task<ErrorOr<TModel>> UpdateByIdAsync(TModel entity, TId id);
} 