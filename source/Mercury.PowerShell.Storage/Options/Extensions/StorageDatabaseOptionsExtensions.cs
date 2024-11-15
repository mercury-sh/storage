using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Mercury.PowerShell.Storage.IO;

namespace Mercury.PowerShell.Storage.Options.Extensions;

[ExcludeFromCodeCoverage]
internal static class StorageDatabaseOptionsExtensions {
  /// <summary>
  ///   Gets the storage database file path.
  /// </summary>
  /// <param name="options">The options.</param>
  /// <returns>The storage database file path.</returns>
  public static AbsolutePath GetAbsolutePath(this StorageDatabaseOptions options)
    => FileSystem.Current.DatabaseDirectory / $"{options.FileName}.db3";

  /// <summary>
  ///   Validates the storage database options.
  /// </summary>
  /// <param name="options">The options.</param>
  /// <exception cref="AggregateException">Thrown when the options are invalid.</exception>
  public static void Validate(this StorageDatabaseOptions options) {
    object instance = options;
    var validationContext = new ValidationContext(instance);
    var validationResults = new List<ValidationResult>();

    if (!Validator.TryValidateObject(instance, validationContext, validationResults, true)) {
      throw new AggregateException(validationResults.Select(validationResult => new ValidationException(validationResult.ErrorMessage)));
    }
  }
}
