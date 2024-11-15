// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Mercury.PowerShell.Storage.Extensions;

namespace Mercury.PowerShell.Storage.IO.Extensions;

/// <summary>
///   Extension methods for <see cref="AbsolutePath" />.
/// </summary>
public static partial class AbsolutePathExtensions {
  /// <summary>
  ///   Deletes the file when existent.
  /// </summary>
  public static void DeleteFile(this AbsolutePath path) {
    if (!path.FileExists()) {
      return;
    }

    File.SetAttributes(path, FileAttributes.Normal);
    File.Delete(path);
  }

  /// <summary>
  ///   Deletes the directory recursively when existent.
  /// </summary>
  public static void DeleteDirectory(this AbsolutePath path) {
    ArgumentNullException.ThrowIfNull(path);

    if (!path.DirectoryExists()) {
      return;
    }

    Directory.GetFiles(path, "*", SearchOption.AllDirectories).ForEach(x => File.SetAttributes(x, FileAttributes.Normal));
    Directory.Delete(path, true);
  }

  /// <summary>
  ///   Deletes directories recursively when existent.
  /// </summary>
  public static void DeleteFiles(this IEnumerable<AbsolutePath> paths)
    => paths.ForEach(path => path.DeleteFile());

  /// <summary>
  ///   Deletes directories recursively when existent.
  /// </summary>
  public static void DeleteDirectories(this IEnumerable<AbsolutePath> paths)
    => paths.ForEach(path => path.DeleteDirectory());
}
