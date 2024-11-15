// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Mercury.PowerShell.Storage.IO.Abstractions;
using static Mercury.PowerShell.Storage.IO.Prelude;

namespace Mercury.PowerShell.Storage.IO;

/// <summary>
///   Represents an absolute path without distinction between files and directories.
/// </summary>
[Serializable]
[TypeConverter(typeof(AbsolutePathTypeConverter))]
[DebuggerDisplay("Path = {ToString(),nq}")]
public sealed class AbsolutePath : IEquatable<AbsolutePath> {
  private readonly string _path;

  private AbsolutePath(string path)
    => _path = NormalizePath(path);

  /// <summary>
  ///   Returns the name of the file or directory.
  /// </summary>
  public string Name => Path.GetFileName(_path);

  /// <summary>
  ///   Returns the name of the file without extension.
  /// </summary>
  public string NameWithoutExtension => Path.GetFileNameWithoutExtension(_path);

  /// <summary>
  ///   Returns the extension of the file with dot.
  /// </summary>
  public string Extension => Path.GetExtension(_path);

  /// <summary>
  ///   Returns the parent path (directory).
  /// </summary>
  public AbsolutePath? Parent => !IsWindowsRoot(_path.TrimEnd(WINDOWS_SEPARATOR)) &&
                                 !IsUnixLikeRoot(_path)
    ? this / ".."
    : null;

  /// <inheritdoc />
  public bool Equals(AbsolutePath? other) {
    var stringComparison = HasWindowsRoot(_path)
      ? StringComparison.OrdinalIgnoreCase
      : StringComparison.Ordinal;
    return string.Equals(_path, other?._path, stringComparison);
  }

  /// <summary>
  ///   Creates a new instance of <see cref="AbsolutePath" />.
  /// </summary>
  /// <param name="path">The path.</param>
  /// <returns>The new instance of <see cref="AbsolutePath" />.</returns>
  public static AbsolutePath Create(string path)
    => new(path);

  [return: NotNullIfNotNull(nameof(path))]
  public static implicit operator AbsolutePath?(string? path) {
    if (path is null) {
      return null;
    }

    if (!HasPathRoot(path)) {
      throw new NotSupportedException($"Path '{path}' must be rooted");
    }

    return new AbsolutePath(path);
  }

  [return: NotNullIfNotNull(nameof(path))]
  public static implicit operator string?(AbsolutePath? path)
    => path?.ToString();

  public static AbsolutePath operator /(AbsolutePath left, string right) {
    ArgumentNullException.ThrowIfNull(left, nameof(left));
    ArgumentException.ThrowIfNullOrEmpty(right, nameof(right));

    return new AbsolutePath(Combine(left, right));
  }

  public static AbsolutePath operator +(AbsolutePath left, string? right)
    => new(left.ToString() + right);

  public static bool operator ==(AbsolutePath a, AbsolutePath b)
    => EqualityComparer<AbsolutePath>.Default.Equals(a, b);

  public static bool operator !=(AbsolutePath a, AbsolutePath b)
    => !EqualityComparer<AbsolutePath>.Default.Equals(a, b);

  /// <inheritdoc />
  public override bool Equals(object? obj) {
    if (Equals(null, obj)) {
      return false;
    }

    if (Equals(this, obj)) {
      return true;
    }

    return obj.GetType() == GetType() &&
           Equals((AbsolutePath)obj);
  }

  /// <inheritdoc />
  public override int GetHashCode()
    => _path.GetHashCode();

  /// <inheritdoc />
  public override string ToString()
    => _path;

  internal sealed class AbsolutePathTypeConverter : TypeConverter {
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
      => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
      => value switch {
        string stringValue => HasPathRoot(stringValue)
          ? (AbsolutePath?)stringValue
          : Environment.CurrentDirectory + stringValue,
        null => null,
        var _ => base.ConvertFrom(context, culture, value)
      };
  }
}
