// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Options.Abstractions;
using Mercury.PowerShell.Storage.Options.Enums;
using SQLite;

namespace Mercury.PowerShell.Storage.Options;

[ExcludeFromCodeCoverage]
internal sealed class StorageDatabaseOptionsBuilder : IOptionsBuilder {
  private SQLiteConnectionAPI _connectionApi;
  private string? _fileName;
  private SQLiteOpenFlags _flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;

  /// <inheritdoc />
  public IOptionsFlags UseFileName(string? name = null) {
    _fileName = name;

    return this;
  }

  /// <inheritdoc />
  public void UseAPI(SQLiteConnectionAPI connectionApi)
    => _connectionApi = connectionApi;

  /// <inheritdoc />
  public IOptionsAPI UseFlags(SQLiteOpenFlags flags) {
    _flags = flags;

    return this;
  }

  /// <inheritdoc />
  public StorageDatabaseOptions ApplyAndBuild()
    => new() {
      FileName = _fileName ?? string.Empty,
      SQLiteConnectionAPI = _connectionApi,
      SQLiteOpenFlags = _flags
    };
}
