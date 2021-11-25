// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions
{
    /// <summary>
    /// Options for configuring state storage providers.
    /// </summary>
    public class StateStorageProviderOptions
    {
        /// <summary>
        /// Gets or sets the state store name.
        /// </summary>
        public string StoreName { get; set; } = default!;
    }
}
