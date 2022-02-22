// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Dapr;

using System.Threading;
using System.Threading.Tasks;
using global::Dapr.Client;
using Rixian.Extensions.ApplicationServices.Abstractions;

/// <summary>
/// Default pub/sub provider backed by Dapr.
/// </summary>
internal class DefaultPubSubProvider : IPubSubProvider
{
    private readonly DaprClient dapr;
    private readonly PubSubProviderOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultPubSubProvider"/> class.
    /// </summary>
    /// <param name="dapr">The Dapr client.</param>
    /// <param name="options">The options to configure this provider.</param>
    public DefaultPubSubProvider(DaprClient dapr, PubSubProviderOptions options)
    {
        this.dapr = dapr;
        this.options = options;
    }

    /// <inheritdoc/>
    public async Task PublishEventAsync<TData>(string eventName, TData data, CancellationToken cancellationToken = default)
    {
        await this.dapr.PublishEventAsync(this.options.PubSubName, eventName, data, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task PublishEventAsync(string eventName, CancellationToken cancellationToken = default)
    {
        await this.dapr.PublishEventAsync(this.options.PubSubName, eventName, cancellationToken);
    }
}
