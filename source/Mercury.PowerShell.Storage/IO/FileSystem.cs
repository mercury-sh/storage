// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Mercury.PowerShell.Storage.IO.Abstractions;

namespace Mercury.PowerShell.Storage.IO;

/// <inheritdoc cref="IFileSystem" />
public static class FileSystem {
  private static IFileSystem? _currentImplementation;

  /// <summary>
  ///   Gets the current file system implementation.
  /// </summary>
  public static IFileSystem Current => _currentImplementation ??= new FileSystemImplementation();
}
