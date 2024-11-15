// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Mercury.PowerShell.Storage.IO.Extensions;

/// <summary>
///   Extensions methods for <see cref="AbsolutePath" />.
/// </summary>
public static partial class AbsolutePathExtensions {
  /// <summary>
  ///   Indicates whether a file or directory exists. The variable or member should indicate whether it is a file (<c>*file</c>,
  ///   <c>*executable</c>, <c>*exe</c>, <c>*script</c>), or a directory (<c>*directory</c>, <c>*dir</c>, <c>*folder</c>).
  /// </summary>
  [Pure]
  public static bool Exists(this AbsolutePath path, [CallerArgumentExpression("path")] string? expression = null) {
    var fileAssociations = new[] { "file", "executable", "exe", "script", "archive" };
    var directoryAssociations = new[] { "directory", "dir", "folder" };

    if (fileAssociations.Contains(expression, StringComparer.OrdinalIgnoreCase)) {
      return path.FileExists();
    }

    if (directoryAssociations.Contains(expression, StringComparer.OrdinalIgnoreCase)) {
      return path.DirectoryExists();
    }

    throw new ArgumentException($"Cannot infer from argument '{expression}' if either file or directory must exist");
  }

  /// <summary>
  ///   Indicates whether the file exists.
  /// </summary>
  [Pure]
  public static bool FileExists(this AbsolutePath? path)
    => File.Exists(path);

  /// <summary>
  ///   Indicates whether the directory exists.
  /// </summary>
  [Pure]
  public static bool DirectoryExists(this AbsolutePath? path)
    => Directory.Exists(path);

  /// <summary>
  ///   Indicates whether the directory contains a file (<c>*</c> as wildcard) using <see cref="SearchOption.TopDirectoryOnly" />.
  /// </summary>
  [Pure]
  public static bool ContainsFile(this AbsolutePath? path, string pattern, SearchOption options = SearchOption.TopDirectoryOnly) {
    ArgumentNullException.ThrowIfNull(path, nameof(path));

    return DirectoryExists(path) &&
           path.ToDirectoryInfo().GetFiles(pattern, options).Length != 0;
  }

  /// <summary>
  ///   Indicates whether the directory contains a directory (<c>*</c> as wildcard) using <see cref="SearchOption.TopDirectoryOnly" />.
  /// </summary>
  [Pure]
  public static bool ContainsDirectory(this AbsolutePath? path, string pattern, SearchOption options = SearchOption.TopDirectoryOnly) {
    ArgumentNullException.ThrowIfNull(path, nameof(path));

    return DirectoryExists(path) &&
           path.ToDirectoryInfo().GetDirectories(pattern, options).Length != 0;
  }

  /// <summary>
  ///   Returns the path if a file or directory exists. The variable or member should indicate whether it is a file (<c>*file</c>,
  ///   <c>*executable</c>, <c>*exe</c>, <c>*script</c>), or a directory (<c>*directory</c>, <c>*dir</c>, <c>*folder</c>).
  /// </summary>
  [Pure]
  public static AbsolutePath? Existing(this AbsolutePath path, [CallerArgumentExpression("path")] string? expression = null)
    => path.Exists(expression) ? path : null;

  /// <summary>
  ///   Returns the path if the file exists.
  /// </summary>
  [Pure]
  public static AbsolutePath? ExistingFile(this AbsolutePath? path)
    => path.FileExists() ? path : null;

  /// <summary>
  ///   Returns the path if the directory exists.
  /// </summary>
  [Pure]
  public static AbsolutePath? ExistingDirectory(this AbsolutePath? path)
    => path.DirectoryExists() ? path : null;

  /// <summary>
  ///   Returns all existing files.
  /// </summary>
  [Pure]
  public static IEnumerable<AbsolutePath> WhereFileExists(this IEnumerable<AbsolutePath> paths)
    => paths.Where(path => path.FileExists());

  /// <summary>
  ///   Returns all existing directories.
  /// </summary>
  [Pure]
  public static IEnumerable<AbsolutePath> WhereDirectoryExists(this IEnumerable<AbsolutePath> paths)
    => paths.Where(path => path.DirectoryExists());
}
