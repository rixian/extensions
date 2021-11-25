// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using global::Dapr.Client;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Rixian.Extensions.ApplicationServices.Dapr;

    /// <summary>
    /// Extensions for the IServiceCollection interface to register caching dependencies.
    /// </summary>
    public static class ApplicationServicesServiceCollectionExtensions
    {
        /// <summary>
        /// Registers dependencies with the IServiceCollection instance.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to use.</param>
        /// <param name="dapr">The Dapr client instance.</param>
        /// <param name="pubsubName">The Dapr name of the pubsub service.</param>
        /// <param name="globalStorageName">The Dapr name for the global state store.</param>
        /// <param name="sharedStorageName">The Dapr name for the shared state store.</param>
        /// <param name="appScopedStorageName">The Dapr name for the application scoped state store.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection AddRixianApplicationServices(this IServiceCollection services, DaprClient dapr, string pubsubName, string globalStorageName, string sharedStorageName, string appScopedStorageName)
        {
            services.TryAddSingleton(dapr);
            services.AddStateStorage<DefaultStateStorageProvider>(globalStorageName, sharedStorageName, appScopedStorageName, (svc, o) => new DefaultStateStorageProvider(svc.GetRequiredService<DaprClient>(), o!));
            services.AddPubSub<DefaultPubSubProvider>(pubsubName, (svc, o) => new DefaultPubSubProvider(svc.GetRequiredService<DaprClient>(), o!));

            services.AddManagedCaching();

            return services;
        }
    }
}
