// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Reflection;
using Mercury.PowerShell.Storage.Abstractions;
using Mercury.PowerShell.Storage.IO.Extensions;
using Mercury.PowerShell.Storage.Options;
using Mercury.PowerShell.Storage.Options.Enums;
using Mercury.PowerShell.Storage.Options.Extensions;
using SQLite;

namespace Mercury.PowerShell.Storage;

internal sealed class ContextProvider : IContextProvider {
  public ContextProvider(StorageDatabaseOptionsRegistrar registrar) {
    ArgumentNullException.ThrowIfNull(registrar, nameof(registrar));

    var options = registrar.Options;
    var filePath = options.GetAbsolutePath();

    filePath.Parent?.CreateDirectory();

    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    var entities = assemblies
      .SelectMany(assembly => assembly.GetTypes())
      .Where(type => type is { IsClass: true, IsAbstract: false } &&
                     type.GetCustomAttribute<TableAttribute>() is not null)
      .ToArray();

    switch (options.SQLiteConnectionAPI) {
      case SQLiteConnectionAPI.Asynchronous:
        AsynchronousConnection = new SQLiteAsyncConnection(filePath, options.SQLiteOpenFlags);
        AsynchronousConnection.CreateTablesAsync(CreateFlags.AllImplicit, entities).Wait();
        break;
      case SQLiteConnectionAPI.Synchronous:
        SynchronousConnection = new SQLiteConnection(filePath, options.SQLiteOpenFlags);
        SynchronousConnection.CreateTables(CreateFlags.AllImplicit, entities);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(options.SQLiteConnectionAPI), options.SQLiteConnectionAPI, null);
    }
  }

  /// <inheritdoc />
  public SQLiteAsyncConnection? AsynchronousConnection { get; private set; }

  /// <inheritdoc />
  public SQLiteConnection? SynchronousConnection { get; private set; }

  /// <inheritdoc />
  public void Dispose() {
    SynchronousConnection?.Dispose();
    SynchronousConnection = null;
  }

  /// <inheritdoc />
  public async ValueTask DisposeAsync() {
    if (AsynchronousConnection is not null) {
      await AsynchronousConnection.CloseAsync();
      AsynchronousConnection = null;
    }

    Dispose();
  }
}
