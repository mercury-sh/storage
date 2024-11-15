// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Mercury.PowerShell.Storage.IO.Extensions;

/// <summary>
///   Extensions methods for <see cref="AbsolutePath" />.
/// </summary>
public static partial class AbsolutePathExtensions {
  /// <summary>
  ///   Creates the correlating <see cref="FileInfo" />.
  /// </summary>
  [Pure]
  [return: NotNullIfNotNull(nameof(path))]
  public static FileInfo? ToFileInfo(this AbsolutePath? path)
    => path is not null ? new FileInfo(path) : null;

  /// <summary>
  ///   Creates the correlating <see cref="DirectoryInfo" />.
  /// </summary>
  [Pure]
  [return: NotNullIfNotNull(nameof(path))]
  public static DirectoryInfo? ToDirectoryInfo(this AbsolutePath? path)
    => path is not null ? new DirectoryInfo(path) : null;
}
