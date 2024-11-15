// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Mercury.PowerShell.Storage.Options.Abstractions;

/// <summary>
///   Represents the builder for the storage database options.
/// </summary>
public interface IOptionsBuilder : IOptionsFileName, IOptionsFlags, IOptionsApply;
