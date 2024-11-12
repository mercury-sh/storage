// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Linq.Expressions;

namespace Mercury.PowerShell.Storage.Abstractions;

public interface IAsyncRepository<TEntity> : IReadOnlyAsyncRepository<TEntity> where TEntity : BaseEntity, new() {
  /// <summary>
  ///   Converts the repository to a read-only repository.
  /// </summary>
  /// <returns>A read-only repository.</returns>
  IReadOnlyAsyncRepository<TEntity> AsReadOnly();

  /// <summary>
  ///   Adds an entity to the repository.
  /// </summary>
  /// <param name="entity">The entity to add.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="entity" /> is <c>null</c>.</exception>
  Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Adds a range of entities to the repository.
  /// </summary>
  /// <param name="entities">The entities to add.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="entities" /> is <c>null</c>.</exception>
  Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Updates an entity in the repository.
  /// </summary>
  /// <param name="id">The identifier of the entity to update.</param>
  /// <param name="action">The action to update the entity.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="id" /> or <paramref name="action" /> is <c>null</c>.</exception>
  Task UpdateAsync(Ulid id, Action<TEntity> action, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Updates an entity in the repository.
  /// </summary>
  /// <param name="entity">The entity to update.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="entity" /> is <c>null</c>.</exception>
  Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Updates a range of entities in the repository.
  /// </summary>
  /// <param name="entities">The entities to update.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="entities" /> is <c>null</c>.</exception>
  Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Updates a range of entities in the repository.
  /// </summary>
  /// <param name="predicate">The predicate to filter the entities to update.</param>
  /// <param name="action">The action to update the entities.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="predicate" /> or <paramref name="action" /> is <c>null</c>.</exception>
  Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Deletes an entity from the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to delete.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="id" /> is <c>null</c>.</exception>
  Task DeleteAsync(Ulid id, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Deletes an entity from the repository.
  /// </summary>
  /// <param name="entity">The entity to delete.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="entity" /> is <c>null</c>.</exception>
  Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Deletes a range of entities from the repository.
  /// </summary>
  /// <param name="entities">The entities to delete.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="entities" /> is <c>null</c>.</exception>
  Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Deletes a range of entities from the repository.
  /// </summary>
  /// <param name="predicate">The predicate to filter the entities to delete.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Deletes a range of entities from the repository by their identifiers.
  /// </summary>
  /// <param name="identifiers">The identifiers of the entities to delete.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <exception cref="ArgumentNullException">If the <paramref name="identifiers" /> is <c>null</c>.</exception>
  Task DeleteAsync(IEnumerable<Ulid> identifiers, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Deletes all entities from the repository.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  Task TruncateAsync(CancellationToken cancellationToken = default);
}
