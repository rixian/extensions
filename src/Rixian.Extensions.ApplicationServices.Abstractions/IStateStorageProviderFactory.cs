// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions;

using Rixian.Extensions.Errors;

/// <summary>
/// A factory that can create IStateStorageProvider instances.
/// </summary>
public interface IStateStorageProviderFactory
{
    /// <summary>
    /// Creates an instance of an IStateStorageProvider using the given logical name.
    /// </summary>
    /// <param name="name">The logical name of the IStateStorageProvider to create.</param>
    /// <returns>An instance of an IStateStorageProvider.</returns>
    Result<IStateStorageProvider> GetStateStorageProvider(string name);
}
