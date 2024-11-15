// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.Options.DataAnnotations;
using Mercury.PowerShell.Storage.Options.Enums;
using SQLite;
using DataAnnotationsMaxLength = System.ComponentModel.DataAnnotations.MaxLengthAttribute;

namespace Mercury.PowerShell.Storage.Options;

/// <summary>
///   Options for the storage database.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public readonly record struct StorageDatabaseOptions {
  /// <summary>
  ///   The file name of the database.
  /// </summary>
  /// <remarks>
  ///   Everything besides the file name will be ignored.
  ///   <br />
  ///   The default file extension is: <c>.db3</c>
  /// </remarks>
  [Required(ErrorMessage = "The file name is required.")]
  [NotNullOrEmpty(ErrorMessage = "The file name cannot be null or empty.")]
  [DataAnnotationsMaxLength(255, ErrorMessage = "The file name must have at most 255 characters.")]
  public required string FileName { get; init; }

  /// <summary>
  ///   The flags to open the database.
  /// </summary>
  /// <remarks>
  ///   The default flags are: <c>Create | ReadWrite | SharedCache</c>
  /// </remarks>
  public SQLiteOpenFlags SQLiteOpenFlags { get; init; }

  /// <summary>
  ///   The SQLite connection API to use.
  /// </summary>
  [Required(ErrorMessage = "The SQLite API is required.")]
  [EnumDataType(typeof(SQLiteConnectionAPI), ErrorMessage = "The SQLite API is invalid.")]
  public SQLiteConnectionAPI SQLiteConnectionAPI { get; init; }
}
