// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Abstractions;
using Mercury.PowerShell.Storage.Exceptions;
using Mercury.PowerShell.Storage.Options;
using Mercury.PowerShell.Storage.Reflection;

namespace Mercury.PowerShell.Storage;

/// <summary>
///   Utility methods for using in a module.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ModuleUtility {
  /// <summary>
  ///   Initializes the context provider with the specified configuration.
  /// </summary>
  /// <param name="configure">The configuration.</param>
  /// <param name="customRootDirectory">The root directory, if different from the default.</param>
  /// <remarks>Should be called when the module is imported.</remarks>
  public static void OnImport(StorageDatabaseOptions.Configure? configure = null, string? customRootDirectory = null) {
    if (configure is not null) {
      SQLitePortableClassLibrary.EnsureLoaded();
      var registrar = new StorageDatabaseOptionsRegistrar(configure);
      var provider = new ContextProvider(registrar);

      ModuleState.ContextProvider = provider;
    }

    if (!string.IsNullOrEmpty(customRootDirectory)) {
      ModuleState.RootDirectory = customRootDirectory;
    }
  }

  /// <summary>
  ///   Gets an asynchronous repository for the specified entity.
  /// </summary>
  /// <typeparam name="TEntity">The entity type.</typeparam>
  /// <returns>The asynchronous repository.</returns>
  /// <exception cref="ContextProviderNotInitializedException">The context provider is not initialized.</exception>
  public static IAsyncRepository<TEntity> GetAsyncRepository<TEntity>() where TEntity : BaseEntity, new() {
    ContextProviderNotInitializedException.ThrowIfNull(ModuleState.ContextProvider);

    return new AsyncRepository<TEntity>(ModuleState.ContextProvider);
  }

  /// <summary>
  ///   Gets a read-only asynchronous repository for the specified entity.
  /// </summary>
  /// <typeparam name="TEntity">The entity type.</typeparam>
  /// <returns>The read-only asynchronous repository.</returns>
  public static IReadOnlyAsyncRepository<TEntity> GetReadOnlyAsyncRepository<TEntity>() where TEntity : BaseEntity, new()
    => GetAsyncRepository<TEntity>().AsReadOnly();

  /// <summary>
  ///   Gets a repository for the specified entity.
  /// </summary>
  /// <typeparam name="TEntity">The entity type.</typeparam>
  /// <returns>The repository.</returns>
  /// <exception cref="InvalidOperationException">The context provider is not initialized.</exception>
  public static IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new() {
    ContextProviderNotInitializedException.ThrowIfNull(ModuleState.ContextProvider);

    return new Repository<TEntity>(ModuleState.ContextProvider);
  }

  /// <summary>
  ///   Gets a read-only repository for the specified entity.
  /// </summary>
  /// <typeparam name="TEntity">The entity type.</typeparam>
  /// <returns>The read-only repository.</returns>
  public static IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : BaseEntity, new()
    => GetRepository<TEntity>().AsReadOnly();
}
