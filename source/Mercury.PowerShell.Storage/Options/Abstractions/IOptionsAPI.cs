// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Options.Enums;

namespace Mercury.PowerShell.Storage.Options.Abstractions;

/// <summary>
///   Represents the builder for the storage database options.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public interface IOptionsAPI {
  /// <summary>
  ///   Use the specified API for the connection.
  /// </summary>
  /// <param name="connectionApi">The connection API.</param>
  void UseAPI(SQLiteConnectionAPI connectionApi);
}
