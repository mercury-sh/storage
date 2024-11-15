// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Mercury.PowerShell.Storage.IO.Extensions;

/// <summary>
///   Extensions methods for <see cref="AbsolutePath" />.
/// </summary>
public static partial class AbsolutePathExtensions {
  /// <summary>
  ///   Returns all files below the directory.
  /// </summary>
  public static IEnumerable<AbsolutePath> GetFiles(this AbsolutePath path, string pattern = "*", int depth = 1, FileAttributes attributes = 0) {
    switch (depth) {
      case < 0:
        throw new ArgumentOutOfRangeException(nameof(depth), "The depth must be greater than or equal to zero.");
      case 0:
        return [];
      default: {
        var files = Directory.EnumerateFiles(path, pattern, SearchOption.TopDirectoryOnly)
          .Where(x => (File.GetAttributes(x) & attributes) == attributes)
          .OrderBy(x => x)
          .Select(AbsolutePath.Create);

        return files.Concat(path.GetDirectories(depth: depth - 1).SelectMany(x => x.GetFiles(pattern, attributes: attributes)));
      }
    }
  }

  /// <summary>
  ///   Returns all directories below the directory.
  /// </summary>
  public static IEnumerable<AbsolutePath> GetDirectories(this AbsolutePath path, string pattern = "*", int depth = 1, FileAttributes attributes = 0) {
    if (depth < 0) {
      throw new ArgumentOutOfRangeException(nameof(depth), "The depth must be greater than or equal to zero.");
    }

    var paths = new string[] { path };
    while (paths.Length != 0 &&
           depth > 0) {
      var matchingDirectories = paths
        .SelectMany(x => Directory.EnumerateDirectories(x, pattern, SearchOption.TopDirectoryOnly))
        .Where(x => (File.GetAttributes(x) & attributes) == attributes)
        .OrderBy(x => x)
        .Select(AbsolutePath.Create).ToList();

      foreach (var matchingDirectory in matchingDirectories) {
        yield return matchingDirectory;
      }

      depth--;
      paths = paths.SelectMany(x => Directory.GetDirectories(x, "*", SearchOption.TopDirectoryOnly)).ToArray();
    }
  }
}
