// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;

namespace Mercury.PowerShell.Storage.Options.Enums;

/// <summary>
///   The SQLite connection API.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum SQLiteConnectionAPI {
  /// <summary>
  ///   The asynchronous API.
  /// </summary>
  Asynchronous = 1 << 0,

  /// <summary>
  ///   The synchronous API.
  /// </summary>
  Synchronous = 1 << 1
}
