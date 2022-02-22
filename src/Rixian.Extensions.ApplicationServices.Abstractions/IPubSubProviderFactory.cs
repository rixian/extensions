// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions
{
    using Rixian.Extensions.Errors;

    /// <summary>
    /// A factory that can create IPubSubProvider instances.
    /// </summary>
    public interface IPubSubProviderFactory
    {
        /// <summary>
        /// Creates an instance of an IPubSubProvider using the given logical name.
        /// </summary>
        /// <param name="name">The logical name of the IPubSubProvider to create.</param>
        /// <returns>An instance of an IPubSubProvider.</returns>
        Result<IPubSubProvider> GetPubSubProvider(string name);
    }
}
