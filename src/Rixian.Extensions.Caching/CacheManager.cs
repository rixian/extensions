// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Caching
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Rixian.Extensions.ApplicationServices.Abstractions;
    using Rixian.Extensions.Errors;
    using static Rixian.Extensions.Errors.Prelude;

    /// <summary>
    /// Manages activities against the cache.
    /// </summary>
    public class CacheManager : ICacheProvider
    {
        /// <summary>
        /// The error code used in the event of a cache miss.
        /// </summary>
        public static readonly string CacheMissErrorCode = "cache.miss";

        /// <summary>
        /// The error code used in the event of an unknown value. For example: Unable to deserialize to a specific type.
        /// </summary>
        public static readonly string CacheUnknownValueErrorCode = "cache.unknown_value";

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly IOptions<CacheManagerOptions> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManager"/> class.
        /// </summary>
        /// <param name="memoryCache">Instace of the IMemoryCache interface.</param>
        /// <param name="distributedCache">Instace of the IDistributedCache interface.</param>
        /// <param name="options">Options for configuring the cache manager.</param>
        public CacheManager(IMemoryCache memoryCache, IDistributedCache distributedCache, IOptions<CacheManagerOptions> options)
        {
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.options = options;
        }

        /// <inheritdoc/>
        public Task RemoveAsync(string key) => this.RemoveAsync(key, default);

        /// <inheritdoc/>
        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            using Activity? activity = InternalUtil.ActivitySource.StartActivity("cache:remove");
            activity?.AddTag("key", key);

            this.memoryCache.Remove(key);
            await this.distributedCache.RemoveAsync(key, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RefreshAsync(string key) => this.RefreshAsync(key, default);

        /// <inheritdoc/>
        public async Task RefreshAsync(string key, CancellationToken cancellationToken)
        {
            using Activity? activity = InternalUtil.ActivitySource.StartActivity("cache:refresh");
            activity?.AddTag("key", key);

            await this.distributedCache.RefreshAsync(key, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Result<T>> GetOrSetAsync<T>(string key, EntityCacheOptions cacheOptions, Func<CancellationToken, Task<Result<T>>> getValueAsync) => this.GetOrSetAsync(key, cacheOptions, getValueAsync, default);

        /// <inheritdoc/>
        public async Task<Result<T>> GetOrSetAsync<T>(string key, EntityCacheOptions cacheOptions, Func<CancellationToken, Task<Result<T>>> getValueAsync, CancellationToken cancellationToken)
        {
            using Activity? activity = InternalUtil.ActivitySource.StartActivity("cache:getorset");

            if (getValueAsync is null)
            {
                throw new ArgumentNullException(nameof(getValueAsync));
            }

            Result<T> result = await this.GetAsync<T>(key, cancellationToken).ConfigureAwait(false);
            if (result.IsSuccess)
            {
                activity?.AddEvent(new ActivityEvent("cache:got_value"));
                return result;
            }
            else
            {
                if (result.Error?.Code == CacheMissErrorCode)
                {
                    activity?.AddEvent(new ActivityEvent("cache:missed_value"));

                    Result<T> newValue = await getValueAsync(cancellationToken).ConfigureAwait(false);
                    activity?.AddEvent(new ActivityEvent("cache:fetched_current_value"));

                    if (newValue.IsSuccess && newValue.Value is { })
                    {
                        await this.SetAsync(key, newValue.Value, cacheOptions, cancellationToken).ConfigureAwait(false);
                        activity?.AddEvent(new ActivityEvent("cache:set_cache_with_current_value"));
                    }

                    return newValue;
                }
                else
                {
                    return result;
                }
            }
        }

        /// <inheritdoc/>
        public Task<Result<T>> GetAsync<T>(string key) => this.GetAsync<T>(key, default);

        /// <inheritdoc/>
        public async Task<Result<T>> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            using Activity? activity = InternalUtil.ActivitySource.StartActivity("cache:get");
            activity?.AddTag("key", key);

            if (this.memoryCache.TryGetValue<T>(key, out T t))
            {
                activity?.AddEvent(new ActivityEvent("cache:got_from_memory"));
                return t;
            }
            else
            {
                activity?.AddEvent(new ActivityEvent("cache:miss_from_memory"));

                var content = await this.distributedCache.GetAsync(key, cancellationToken).ConfigureAwait(false);
                if (content == null)
                {
                    activity?.AddEvent(new ActivityEvent("cache:miss_from_remote"));
                    return Error(CacheMissErrorCode, "Cache miss.", key);
                }
                else
                {
                    activity?.AddEvent(new ActivityEvent("cache:got_from_remote"));
                    var json = Encoding.UTF8.GetString(content);
                    T? value = JsonSerializer.Deserialize<T>(json, this.options?.Value?.SerializerOptions);

                    if (value is null)
                    {
                        activity?.AddEvent(new ActivityEvent("cache:deserialize_failed"));
                        return Error(CacheUnknownValueErrorCode, $"Unable to deserialize the cache value for key \"{key}\" to type \"{typeof(T)}\".");
                    }
                    else
                    {
                        return value;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public Task SetAsync<T>(string key, T value) => this.SetAsync<T>(key, value, default, default);

        /// <inheritdoc/>
        public Task SetAsync<T>(string key, T value, EntityCacheOptions options) => this.SetAsync<T>(key, value, options, default);

        /// <inheritdoc/>
        public async Task SetAsync<T>(string key, T value, EntityCacheOptions? options, CancellationToken cancellationToken)
        {
            using Activity? activity = InternalUtil.ActivitySource.StartActivity("cache:set");
            activity?.AddTag("key", key);

            this.memoryCache.Set<T>(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = options?.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = options?.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = options?.SlidingExpiration,
            });
            activity?.AddEvent(new ActivityEvent("cache:set_memory"));

            var valueBytes = JsonSerializer.SerializeToUtf8Bytes(value, this.options?.Value?.SerializerOptions);
            await this.distributedCache.SetAsync(
                key,
                valueBytes,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = options?.AbsoluteExpiration,
                    AbsoluteExpirationRelativeToNow = options?.AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = options?.SlidingExpiration,
                },
                cancellationToken).ConfigureAwait(false);
            activity?.AddEvent(new ActivityEvent("cache:set_remote"));
        }
    }
}
