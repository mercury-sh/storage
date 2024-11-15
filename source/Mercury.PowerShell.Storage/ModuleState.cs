// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Abstractions;
using Mercury.PowerShell.Storage.IO;
using static Mercury.PowerShell.Storage.IO.Prelude;

namespace Mercury.PowerShell.Storage;

[ExcludeFromCodeCoverage]
internal static class ModuleState {
  /// <summary>
  ///   The root directory.
  /// </summary>
  public static AbsolutePath RootDirectory { get; set; }
    = AbsolutePath.Create(Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mercury"));

  /// <summary>
  ///   The context provider.
  /// </summary>
  public static IContextProvider? ContextProvider { get; set; }
}
