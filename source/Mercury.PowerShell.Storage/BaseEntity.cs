// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using SQLite;

namespace Mercury.PowerShell.Storage;

/// <summary>
///   Represents the base entity of the system.
/// </summary>
[Serializable]
public class BaseEntity {
  // Since the type Ulid is not serializable in SQLite-net (and there is no way to extend it), we store it as a string with a backing field.
  // Long or Int types are not used as PK because we would need to save the entity in the database to get the ID.
  // Guid is not used because it's the version 4 of the UUID, and I would like to use the version 7.
  private readonly Ulid _pk = Ulid.NewUlid();

  /// <summary>
  ///   The unique identifier of the entity.
  /// </summary>
  [PrimaryKey]
  [Column("id")]
  [MaxLength(26)]
  public string Id { get => _pk.ToString(); init => _pk = Ulid.Parse(value); }

  /// <summary>
  ///   Gets the primary key of the entity as an <see cref="Ulid" />.
  /// </summary>
  /// <returns>The primary key of the entity.</returns>
  public Ulid GetPrimaryKey()
    => _pk;
}
