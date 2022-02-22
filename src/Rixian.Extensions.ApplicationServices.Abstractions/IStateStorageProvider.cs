// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides state storage capabilities.
/// </summary>
public interface IStateStorageProvider
{
    /// <summary>
    /// Gets the stored state for the provided key.
    /// </summary>
    /// <typeparam name="TValue">The Type of the JSON-serializable data.</typeparam>
    /// <param name="key">The state lookup key.</param>
    /// <param name="metadata">The metadata for the underlying storage system.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>The stored state.</returns>
    public Task<TValue> GetStateAsync<TValue>(string key, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the stored state and associated eTag for the provided key.
    /// </summary>
    /// <typeparam name="TValue">The Type of the JSON-serializable data.</typeparam>
    /// <param name="key">The state lookup key.</param>
    /// <param name="metadata">The metadata for the underlying storage system.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A tuple of the stored state and current eTag.</returns>
    public Task<(TValue Value, string Etag)> GetStateAndETagAsync<TValue>(string key, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the state for the provided key.
    /// </summary>
    /// <typeparam name="TValue">The Type of the JSON-serializable data.</typeparam>
    /// <param name="key">The state lookup key.</param>
    /// <param name="value">The state to store.</param>
    /// <param name="metadata">The metadata for the underlying storage system.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task SaveStateAsync<TValue>(string key, TValue value, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to save the state for the provided key and eTag.
    /// </summary>
    /// <typeparam name="TValue">The Type of the JSON-serializable data.</typeparam>
    /// <param name="key">The state lookup key.</param>
    /// <param name="value">The state to store.</param>
    /// <param name="etag">The eTag to use for concurrency checks.</param>
    /// <param name="metadata">The metadata for the underlying storage system.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A bool indicating if the operation succeeded.</returns>
    public Task<bool> TrySaveStateAsync<TValue>(string key, TValue value, string etag, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleted the stored state for the provided key.
    /// </summary>
    /// <param name="key">The state lookup key.</param>
    /// <param name="metadata">The metadata for the underlying storage system.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task DeleteStateAsync(string key, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to delete the stored state for the provided key and eTag.
    /// </summary>
    /// <param name="key">The state lookup key.</param>
    /// <param name="etag">The eTag to use for concurrency checks.</param>
    /// <param name="metadata">The metadata for the underlying storage system.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A bool indicating if the operation succeeded.</returns>
    public Task<bool> TryDeleteStateAsync(string key, string etag, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);
}
