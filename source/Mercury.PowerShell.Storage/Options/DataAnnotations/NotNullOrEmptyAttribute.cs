// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.ComponentModel.DataAnnotations;

namespace Mercury.PowerShell.Storage.Options.DataAnnotations;

/// <summary>
///   Represents a validation attribute that ensures the value is not null or empty.
/// </summary>
internal sealed class NotNullOrEmptyAttribute : ValidationAttribute {
  /// <inheritdoc />
  public override bool IsValid(object? value)
    => value switch {
      string str => !string.IsNullOrEmpty(str),
      Array array => array.Length > 0,
      var _ => false
    };

  /// <inheritdoc />
  public override string FormatErrorMessage(string name)
    => $"The {name} field must not be null or empty.";
}
