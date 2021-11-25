// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Abstractions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Rixian.Extensions.Errors;

    /// <summary>
    /// Manages activities against the cache.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Gets the value from the cache, otherwise returns an error.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Result<T>> GetAsync<T>(string key);

        /// <summary>
        /// Gets the value from the cache, otherwise returns an error.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="cancellationToken">The System.Threading.CancellationToken used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Result<T>> GetAsync<T>(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the value from the cache or sets it if no value exists.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="cacheOptions">The cache options to use.</param>
        /// <param name="getValueAsync">Delegate to fetch the value to be cached in the event of a cache miss.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Throws if the getValueAsync delegate is null.</exception>
        Task<Result<T>> GetOrSetAsync<T>(string key, EntityCacheOptions cacheOptions, Func<CancellationToken, Task<Result<T>>> getValueAsync);

        /// <summary>
        /// Gets the value from the cache or sets it if no value exists.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="cacheOptions">The cache options to use.</param>
        /// <param name="getValueAsync">Delegate to fetch the value to be cached in the event of a cache miss.</param>
        /// <param name="cancellationToken">The System.Threading.CancellationToken used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Throws if the getValueAsync delegate is null.</exception>
        Task<Result<T>> GetOrSetAsync<T>(string key, EntityCacheOptions cacheOptions, Func<CancellationToken, Task<Result<T>>> getValueAsync, CancellationToken cancellationToken);

        /// <summary>
        /// Refreshes the distributed cache value with the given key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task RefreshAsync(string key);

        /// <summary>
        /// Refreshes the distributed cache value with the given key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="cancellationToken">The System.Threading.CancellationToken used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task RefreshAsync(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the cache value with the given key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// Removes the cache value with the given key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="cancellationToken">The System.Threading.CancellationToken used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task RemoveAsync(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the value in the cache.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to store in the cache.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task SetAsync<T>(string key, T value);

        /// <summary>
        /// Sets the value in the cache.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to store in the cache.</param>
        /// <param name="options">The cache options to use.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task SetAsync<T>(string key, T value, EntityCacheOptions options);

        /// <summary>
        /// Sets the value in the cache.
        /// </summary>
        /// <typeparam name="T">The object type to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to store in the cache.</param>
        /// <param name="options">The cache options to use.</param>
        /// <param name="cancellationToken">The System.Threading.CancellationToken used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task SetAsync<T>(string key, T value, EntityCacheOptions? options, CancellationToken cancellationToken);
    }
}
