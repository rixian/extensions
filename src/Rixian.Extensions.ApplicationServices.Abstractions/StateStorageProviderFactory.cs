// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.ApplicationServices.Dapr;

using System;
using Microsoft.Extensions.Options;
using Rixian.Extensions.ApplicationServices.Abstractions;
using Rixian.Extensions.DependencyInjection;
using Rixian.Extensions.Errors;
using static Rixian.Extensions.Errors.Prelude;

/// <summary>
/// Factory used for creating instances of an ITokenClient.
/// </summary>
internal class StateStorageProviderFactory : GenericFactory<StateStorageProviderOptions, IStateStorageProvider>, IStateStorageProviderFactory
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StateStorageProviderFactory"/> class.
    /// </summary>
    /// <param name="services">The <see cref="IServiceProvider"/>.</param>
    /// <param name="options">The options to use for creating instances of <see cref="IStateStorageProvider"/>.</param>
    /// <param name="factoryOptions">Options for the factory.</param>
    public StateStorageProviderFactory(IServiceProvider services, IOptionsMonitor<StateStorageProviderOptions> options, IOptions<GenericFactoryOptions<StateStorageProviderOptions, IStateStorageProvider>> factoryOptions)
        : base(services, options, factoryOptions)
    {
    }

    /// <inheritdoc/>
    public Result<IStateStorageProvider> GetStateStorageProvider(string name)
    {
        Result<IStateStorageProvider> result = this.GetItem(name);

        return result.IsSuccess switch
        {
            false => result.Cast<IStateStorageProvider>(),
            _ => Result(result.Value!),
        };
    }
}
