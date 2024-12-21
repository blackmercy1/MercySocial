using ErrorOr;
using MercySocial.Application.Common.Repository;
using MercySocial.Domain.Common;
using MercySocial.Domain.Common.Errors;

namespace MercySocial.Application.Common.Service;

/// <summary>
/// Provides a base service implementation for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="TModel">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
/// <typeparam name="TIdType">The underlying type of the identifier.</typeparam>
public abstract class EntityService<TModel, TId, TIdType> : IService<TModel, TId, TIdType>
    where TModel : Entity<TId>
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    /// <summary>
    /// The repository used for accessing the underlying data store.
    /// </summary>
    protected readonly IRepository<TModel, TId> Repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityService{TModel, TId, TIdType}"/> class.
    /// </summary>
    /// <param name="repository">The repository for the entity.</param>
    protected EntityService(IRepository<TModel, TId> repository)
    {
        Repository = repository;
    }

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The entity if found, or an error if not found.</returns>
    public virtual async Task<ErrorOr<TModel>> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        return entity is null 
            ? Errors.Entity.EntityWasNotFound
            : entity;
    }

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="tId">The identifier of the entity to delete.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The deleted entity if successful, or an error if not found.</returns>
    public async Task<ErrorOr<TModel>> DeleteByIdAsync(TId tId, CancellationToken cancellationToken)
    {
        var existingEntity = await Repository.GetByIdAsync(tId, cancellationToken);
        if (existingEntity is null)
            return Errors.Entity.EntityWasNotFound;

        await Repository.DeleteAsync(existingEntity, cancellationToken);

        return existingEntity;
    }

    /// <summary>
    /// Adds a new entity to the data store.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The added entity if successful, or an error if it already exists.</returns>
    public virtual async Task<ErrorOr<TModel>> AddAsync(TModel entity, CancellationToken cancellationToken)
    {
        var existingEntity = await Repository.ExistsByIdAsync(entity.Id, cancellationToken);
        if (existingEntity)
            return Errors.Entity.EntityExists;

        var addedEntity = await Repository.AddAsync(entity, cancellationToken);

        return addedEntity;
    }

    /// <summary>
    /// Deletes an entity by its identifier and confirms the deletion.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>True if the entity was successfully deleted, or an error if not found.</returns>
    public virtual async Task<ErrorOr<bool>> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var existingEntity = await Repository.GetByIdAsync(id, cancellationToken);
        if (existingEntity is null)
            return Errors.Entity.EntityWasNotFound;

        await Repository.DeleteAsync(existingEntity, cancellationToken);

        return true;
    }

    /// <summary>
    /// Updates an entity by its identifier.
    /// </summary>
    /// <param name="entity">The updated entity data.</param>
    /// <param name="id">The identifier of the entity to update.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The updated entity if successful, or an error if not found.</returns>
    public async Task<ErrorOr<TModel>> UpdateByIdAsync(TModel entity, TId id, CancellationToken cancellationToken)
    {
        var existingEntity = await Repository.GetByIdAsync(id, cancellationToken);
        if (existingEntity is null)
            return Errors.Entity.EntityWasNotFound;
        
        await Repository.UpdateByIdAsync(entity, existingEntity, cancellationToken);

        return existingEntity;
    }
}