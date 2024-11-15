// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Mercury.PowerShell.Storage.IO.Abstractions;

namespace Mercury.PowerShell.Storage.IO;

internal sealed class FileSystemImplementation : IFileSystem {
  /// <inheritdoc />
  public AbsolutePath RootDirectory { get; } = ModuleState.RootDirectory;

  /// <inheritdoc />
  public AbsolutePath DatabaseDirectory { get; } = ModuleState.RootDirectory / "Databases";
}
