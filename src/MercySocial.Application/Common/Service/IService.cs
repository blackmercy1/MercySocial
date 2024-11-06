using ErrorOr;
using MercySocial.Domain.Common;

namespace MercySocial.Application.Common.Service;

public interface IService<TModel, in TId, in TIdType>
    where TModel : Entity<TId>
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    Task<ErrorOr<TModel>> AddAsync(TModel entity, CancellationToken cancellationToken);
    Task<ErrorOr<TModel>> UpdateByIdAsync(TModel entity, TId id, CancellationToken cancellationToken);
    Task<ErrorOr<TModel>> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<ErrorOr<TModel>> DeleteByIdAsync(TId tId, CancellationToken cancellationToken);
}