using SQLite;

namespace Mercury.PowerShell.Storage.Options.Abstractions;

/// <summary>
///   Represents the extras interface for the storage database options.
/// </summary>
public interface IOptionsFlags : IOptionsAPI {
  /// <summary>
  ///   Uses the provided flags to build the database connection.
  /// </summary>
  /// <param name="flags">The flags.</param>
  /// <returns>The API builder.</returns>
  IOptionsAPI UseFlags(SQLiteOpenFlags flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
}
