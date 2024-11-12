// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using SQLite;

namespace Mercury.PowerShell.Storage.Abstractions;

/// <summary>
///   Represents the context provider for the storage database.
/// </summary>
public interface IContextProvider : IDisposable, IAsyncDisposable {
  /// <summary>
  ///   The asynchronous connection to the database.
  /// </summary>
  SQLiteAsyncConnection? AsynchronousConnection { get; }

  /// <summary>
  ///   The synchronous connection to the database.
  /// </summary>
  SQLiteConnection? SynchronousConnection { get; }
}
