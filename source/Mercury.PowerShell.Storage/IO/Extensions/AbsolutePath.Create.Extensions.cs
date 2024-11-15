// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;

namespace Mercury.PowerShell.Storage.IO.Extensions;

/// <summary>
///   Extensions methods for <see cref="AbsolutePath" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static partial class AbsolutePathExtensions {
  /// <summary>
  ///   Creates the directory.
  /// </summary>
  public static AbsolutePath CreateDirectory(this AbsolutePath path) {
    Directory.CreateDirectory(path);

    return path;
  }

  /// <summary>
  ///   Creates or cleans the directory.
  /// </summary>
  public static AbsolutePath CreateOrCleanDirectory(this AbsolutePath path) {
    path.DeleteDirectory();
    path.CreateDirectory();

    return path;
  }

  /// <summary>
  ///   Creates (touches) the file. Similar to the UNIX command, the last-write time is updated.
  /// </summary>
  public static AbsolutePath TouchFile(this AbsolutePath path, DateTime? time = null, bool createDirectories = true) {
    if (createDirectories) {
      path.Parent?.CreateDirectory();
    }

    if (!File.Exists(path)) {
      File.WriteAllBytes(path, []);
    }

    File.SetLastWriteTime(path, time ?? DateTime.Now);

    return path;
  }
}
