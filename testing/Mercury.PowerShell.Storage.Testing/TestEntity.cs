// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using SQLite;

namespace Mercury.PowerShell.Storage.Testing;

[Table("test_entity")]
public sealed class TestEntity : BaseEntity {
  [Column("name")]
  public string Name { get; set; } = default!;
}
