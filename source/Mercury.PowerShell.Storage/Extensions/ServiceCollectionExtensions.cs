// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Abstractions;
using Mercury.PowerShell.Storage.Options;
using Mercury.PowerShell.Storage.Options.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Mercury.PowerShell.Storage.Extensions;

/// <summary>
///   Extensions for the <see cref="IServiceCollection" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions {
  /// <summary>
  ///   Adds the storage database to the service collection.
  /// </summary>
  /// <param name="serviceCollection">The service collection.</param>
  /// <param name="configure">The configuration.</param>
  /// <exception cref="ArgumentOutOfRangeException">The SQLite connection API is not supported.</exception>
  /// <returns>The service collection itself.</returns>
  public static IServiceCollection AddStorageDatabase(this IServiceCollection serviceCollection, StorageDatabaseOptions.Configure configure) {
    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    var entities = assemblies
      .SelectMany(assembly => assembly.GetTypes())
      .Where(type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(BaseEntity)));

    var registrar = new StorageDatabaseOptionsRegistrar(configure);
    serviceCollection.AddSingleton<IContextProvider>(_ => new ContextProvider(registrar));

    switch (registrar.Options.SQLiteConnectionAPI) {
      case SQLiteConnectionAPI.Asynchronous:
        entities.ForEach(entity => {
          var repositoryType = typeof(AsyncRepository<>).MakeGenericType(entity);

          serviceCollection.AddTransient(typeof(IAsyncRepository<>).MakeGenericType(entity), repositoryType);
          serviceCollection.AddTransient(typeof(IReadOnlyAsyncRepository<>).MakeGenericType(entity), repositoryType);
        });
        break;
      case SQLiteConnectionAPI.Synchronous:
        entities.ForEach(entity => {
          var repositoryType = typeof(Repository<>).MakeGenericType(entity);

          serviceCollection.AddTransient(typeof(IRepository<>).MakeGenericType(entity), repositoryType);
          serviceCollection.AddTransient(typeof(IReadOnlyRepository<>).MakeGenericType(entity), repositoryType);
        });
        break;

      default:
        throw new ArgumentOutOfRangeException(nameof(registrar.Options.SQLiteConnectionAPI), registrar.Options.SQLiteConnectionAPI, null);
    }

    return serviceCollection;
  }
}
