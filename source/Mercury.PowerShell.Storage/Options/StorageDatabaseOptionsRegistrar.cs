// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Mercury.PowerShell.Storage.Options.Extensions;

namespace Mercury.PowerShell.Storage.Options;

[ExcludeFromCodeCoverage]
internal sealed class StorageDatabaseOptionsRegistrar {
  public StorageDatabaseOptionsRegistrar(StorageDatabaseOptions.Configure configure) {
    var callingAssembly = Assembly.GetEntryAssembly();
    var assemblyName = callingAssembly?.GetName().Name;

    var builder = new StorageDatabaseOptionsBuilder();
    configure.Invoke(builder);

    Options = builder.ApplyAndBuild();

    if (string.IsNullOrEmpty(Options.FileName) &&
        !string.IsNullOrEmpty(assemblyName)) {
      builder.UseFileName(assemblyName);
      configure.Invoke(builder);
      Options = builder.ApplyAndBuild();
    }

    Options.Validate();
  }

  /// <summary>
  ///   The options for the storage database.
  /// </summary>
  public StorageDatabaseOptions Options { get; }
}
