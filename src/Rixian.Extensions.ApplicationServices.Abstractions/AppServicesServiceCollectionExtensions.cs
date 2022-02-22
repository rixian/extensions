// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection;

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Rixian.Extensions.ApplicationServices.Abstractions;
using Rixian.Extensions.ApplicationServices.Dapr;

/// <summary>
/// Extensions for the IServiceCollection interface to register caching dependencies.
/// </summary>
public static class AppServicesServiceCollectionExtensions
{
    /// <summary>
    /// Registers dependencies with the IServiceCollection instance.
    /// </summary>
    /// <typeparam name="TStateStore">The Type of the state store.</typeparam>
    /// <param name="services">The IServiceCollection instance to use.</param>
    /// <param name="globalStorageName">The Dapr name for the global state store.</param>
    /// <param name="sharedStorageName">The Dapr name for the shared state store.</param>
    /// <param name="appScopedStorageName">The Dapr name for the application scoped state store.</param>
    /// <param name="createDefaultStateStore">Delegate for creating a new state store.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddStateStorage<TStateStore>(this IServiceCollection services, string globalStorageName, string sharedStorageName, string appScopedStorageName, Func<IServiceProvider, StateStorageProviderOptions?, TStateStore> createDefaultStateStore)
        where TStateStore : IStateStorageProvider
    {
        services.Configure<StateStorageProviderOptions>("global", o =>
        {
            o.StoreName = globalStorageName;
        });
        services.Configure<StateStorageProviderOptions>("shared", o =>
        {
            o.StoreName = sharedStorageName;
        });
        services.Configure<StateStorageProviderOptions>("appScoped", o =>
        {
            o.StoreName = appScopedStorageName;
        });

        services.TryAddSingleton<StateStorageProviderFactory>();
        services.TryAddSingleton<IStateStorageProviderFactory>(svc => svc.GetRequiredService<StateStorageProviderFactory>());
        services.TryAddSingleton<IFactory<StateStorageProviderOptions, IStateStorageProvider>>(svc => svc.GetRequiredService<StateStorageProviderFactory>());
        services.ConfigureFactory<StateStorageProviderOptions, IStateStorageProvider>((svc, o) => createDefaultStateStore.Invoke(svc, o));
        services.AddTransient<IStateStorageProvider>(svc => svc.GetRequiredService<IStateStorageProviderFactory>().GetStateStorageProvider("appScoped").Value!);

        return services;
    }

    /// <summary>
    /// Registers dependencies with the IServiceCollection instance.
    /// </summary>
    /// <typeparam name="TPubSub">The Type of the pubsub provider.</typeparam>
    /// <param name="services">The IServiceCollection instance to use.</param>
    /// <param name="pubsubName">The Dapr name of the pubsub service.</param>
    /// <param name="createDefaultPubSub">Delegate for creating a new pubsub provider.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddPubSub<TPubSub>(this IServiceCollection services, string pubsubName, Func<IServiceProvider, PubSubProviderOptions?, TPubSub> createDefaultPubSub)
        where TPubSub : IPubSubProvider
    {
        services.Configure<PubSubProviderOptions>(pubsubName, o =>
        {
            o.PubSubName = pubsubName;
        });

        services.TryAddSingleton<PubSubProviderFactory>();
        services.TryAddSingleton<IPubSubProviderFactory>(svc => svc.GetRequiredService<PubSubProviderFactory>());
        services.TryAddSingleton<IFactory<PubSubProviderOptions, IPubSubProvider>>(svc => svc.GetRequiredService<PubSubProviderFactory>());
        services.ConfigureFactory<PubSubProviderOptions, IPubSubProvider>((svc, o) => createDefaultPubSub.Invoke(svc, o));
        services.AddTransient<IPubSubProvider>(svc => svc.GetRequiredService<IPubSubProviderFactory>().GetPubSubProvider(pubsubName).Value!);

        return services;
    }
}
