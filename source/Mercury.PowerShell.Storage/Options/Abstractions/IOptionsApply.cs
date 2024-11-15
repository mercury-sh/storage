namespace Mercury.PowerShell.Storage.Options.Abstractions;

/// <summary>
///   Represents the apply interface for the storage database options.
/// </summary>
public interface IOptionsApply {
  /// <summary>
  ///   Applies the options and builds the storage database options.
  /// </summary>
  /// <returns>The built storage database options.</returns>
  StorageDatabaseOptions ApplyAndBuild();
}
