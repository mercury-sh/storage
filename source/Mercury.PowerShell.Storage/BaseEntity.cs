// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;
using SQLite;

namespace Mercury.PowerShell.Storage;

/// <summary>
///   Represents the base entity of the system.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString(),nq}")]
public abstract class BaseEntity : IComparable<BaseEntity>, IEquatable<BaseEntity> {
  /// <summary>
  ///   The unique identifier of the entity.
  /// </summary>
  [PrimaryKey]
  [Column("id")]
  [MaxLength(36)]
  public Guid Id { get; init; } = Guid.NewGuid();

  /// <inheritdoc />
  public int CompareTo(BaseEntity? other)
    => other is null ? 1 : Id.CompareTo(other.Id);

  /// <inheritdoc />
  public bool Equals(BaseEntity? other)
    => other is not null && Id.Equals(other.Id);

  /// <inheritdoc />
  public sealed override string ToString()
    => Id.ToString();

  /// <inheritdoc />
  public override bool Equals(object? obj)
    => obj is BaseEntity other && Equals(other);

  /// <inheritdoc />
  public override int GetHashCode()
    => Id.GetHashCode();
}
