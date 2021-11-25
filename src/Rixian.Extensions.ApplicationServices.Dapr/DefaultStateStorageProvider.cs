// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Dapr
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Dapr.Client;
    using Rixian.Extensions.ApplicationServices.Abstractions;

    /// <summary>
    /// Default state storage provider for Dapr state.
    /// </summary>
    internal class DefaultStateStorageProvider : IStateStorageProvider
    {
        private readonly DaprClient dapr;
        private readonly StateStorageProviderOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultStateStorageProvider"/> class.
        /// </summary>
        /// <param name="dapr">The Dapr client.</param>
        /// <param name="options">The options to configure this provider.</param>
        public DefaultStateStorageProvider(DaprClient dapr, StateStorageProviderOptions options)
        {
            this.dapr = dapr;
            this.options = options;
        }

        /// <inheritdoc/>
        public async Task DeleteStateAsync(string key, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
        {
            await this.dapr.DeleteStateAsync(this.options.StoreName, key, metadata: metadata, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<(TValue Value, string Etag)> GetStateAndETagAsync<TValue>(string key, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
        {
            return await this.dapr.GetStateAndETagAsync<TValue>(this.options.StoreName, key, metadata: metadata, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TValue> GetStateAsync<TValue>(string key, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
        {
            return await this.dapr.GetStateAsync<TValue>(this.options.StoreName, key, metadata: metadata, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SaveStateAsync<TValue>(string key, TValue value, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
        {
            await this.dapr.SaveStateAsync(this.options.StoreName, key, value, metadata: metadata, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> TryDeleteStateAsync(string key, string etag, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
        {
            return await this.dapr.TryDeleteStateAsync(this.options.StoreName, key, etag, metadata: metadata, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> TrySaveStateAsync<TValue>(string key, TValue value, string etag, IReadOnlyDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
        {
            return await this.dapr.TrySaveStateAsync(this.options.StoreName, key, value, etag, metadata: metadata, cancellationToken: cancellationToken);
        }
    }
}
