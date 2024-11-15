// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Mercury.PowerShell.Storage.IO.Abstractions;

/// <summary>
///   Provides an easy way to access all predefined paths in the system.
/// </summary>
public interface IFileSystem {
  /// <summary>
  ///   Gets the root directory.
  /// </summary>
  AbsolutePath RootDirectory { get; }

  /// <summary>
  ///   Gets the location where all <c>.db3</c> files can be stored.
  /// </summary>
  AbsolutePath DatabaseDirectory { get; }
}
