// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Abstractions;
using Mercury.PowerShell.Storage.Options.Enums;
using SQLite;
using IsNotNull = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Mercury.PowerShell.Storage.Exceptions;

/// <summary>
///   Represents an exception that is thrown when the <see creIContextProviderder.AsynchronousConnection" /> or
///
///   <see cref="IContextProvider.SynchronousConnection" /> has not been established.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class SQLiteConnectionNotEstablishedException(SQLiteConnectionAPI connectionApi)
  : Exception($"The {Enum.GetName(connectionApi)?.ToLower()} connection has not been established.") {
  /// <summary>
  ///   Throws an <see cref="SQLiteConnectionNotEstablishedException" /> if the <see cref="IContextProvider.AsynchronousConnection" /> is null.
  /// </summary>
  /// <param name="sqLiteAsyncConnection">The connection provider.</param>
  /// <exception cref="SQLiteConnectionNotEstablishedException">The asynchronous connection has not been established.</exception>
  public static void ThrowIfNull([IsNotNull] SQLiteAsyncConnection? sqLiteAsyncConnection) {
    if (sqLiteAsyncConnection is null) {
      throw new SQLiteConnectionNotEstablishedException(SQLiteConnectionAPI.Asynchronous);
    }
  }

  /// <summary>
  ///   Throws an <see cref="SQLiteConnectionNotEstablishedException" /> if the <see cref="IContextProvider.SynchronousConnection" /> is null.
  /// </summary>
  /// <param name="sqLiteConnection">The connection provider.</param>
  /// <exception cref="SQLiteConnectionNotEstablishedException">The synchronous connection has not been established.</exception>
  public static void ThrowIfNull([IsNotNull] SQLiteConnection? sqLiteConnection) {
    if (sqLiteConnection is null) {
      throw new SQLiteConnectionNotEstablishedException(SQLiteConnectionAPI.Synchronous);
    }
  }
}
