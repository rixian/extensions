// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions;

/// <summary>
/// Options for the PubSub providers.
/// </summary>
public class PubSubProviderOptions
{
    /// <summary>
    /// Gets or sets the pubsub name.
    /// </summary>
    public string PubSubName { get; set; } = default!;
}
