// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Dapr
{
    using System;
    using Microsoft.Extensions.Options;
    using Rixian.Extensions.ApplicationServices.Abstractions;
    using Rixian.Extensions.DependencyInjection;
    using Rixian.Extensions.Errors;
    using static Rixian.Extensions.Errors.Prelude;

    /// <summary>
    /// Factory used for creating instances of an ITokenClient.
    /// </summary>
    internal class PubSubProviderFactory : GenericFactory<PubSubProviderOptions, IPubSubProvider>, IPubSubProviderFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubProviderFactory"/> class.
        /// </summary>
        /// <param name="services">The <see cref="IServiceProvider"/>.</param>
        /// <param name="options">The options to use for creating instances of <see cref="IPubSubProvider"/>.</param>
        /// <param name="factoryOptions">Options for the factory.</param>
        public PubSubProviderFactory(IServiceProvider services, IOptionsMonitor<PubSubProviderOptions> options, IOptions<GenericFactoryOptions<PubSubProviderOptions, IPubSubProvider>> factoryOptions)
            : base(services, options, factoryOptions)
        {
        }

        /// <inheritdoc/>
        public Result<IPubSubProvider> GetPubSubProvider(string name)
        {
            Result<IPubSubProvider> result = this.GetItem(name);

            if (result.IsSuccess == false)
            {
                return result.Cast<IPubSubProvider>();
            }
            else
            {
                return Result<IPubSubProvider>(result.Value!);
            }
        }
    }
}
