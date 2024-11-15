namespace Mercury.PowerShell.Storage.Options.Abstractions;

/// <summary>
///   Represents the base interface for the storage database options.
/// </summary>
public interface IOptionsFileName {
  /// <summary>
  ///   Uses the provided file name to build the database path.
  /// </summary>
  /// <param name="name">The file name.</param>
  /// <returns>The flags' builder.</returns>
  /// <remarks>
  ///   If not provided, the default file name will be the calling assembly name.
  ///   For example, if the calling assembly is <c>Mercury.PowerShell.Storage</c>, the default file name will be <c>Mercury.PowerShell.Storage.db3</c>.
  /// </remarks>
  IOptionsFlags UseFileName(string? name = null);
}
