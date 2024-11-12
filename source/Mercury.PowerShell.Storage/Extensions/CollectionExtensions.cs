// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;

namespace Mercury.PowerShell.Storage.Extensions;

/// <summary>
///   Extension methods for collections.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class CollectionExtensions {
  /// <summary>
  ///   Performs the specified action on each element of the <see cref="IEnumerable{T}" />.
  /// </summary>
  /// <param name="source">The source collection.</param>
  /// <param name="action">The action to perform on each element.</param>
  /// <typeparam name="T">The type of the elements in the collection.</typeparam>
  public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
    foreach (var item in source) {
      action(item);
    }
  }
}
