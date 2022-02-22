// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides publishing mechanics for a pub/sub system.
/// </summary>
public interface IPubSubProvider
{
    /// <summary>
    /// Publishes an event to the topic.
    /// </summary>
    /// <typeparam name="TData">The data type for the event payload.</typeparam>
    /// <param name="eventName">The name of the event.</param>
    /// <param name="data">The event data to be serialized.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task PublishEventAsync<TData>(string eventName, TData data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes an event to the topic.
    /// </summary>
    /// <param name="eventName">The name of the event.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task PublishEventAsync(string eventName, CancellationToken cancellationToken = default);
}
