// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;
    using Rixian.Extensions.Errors;

    /// <summary>
    /// A generic factory for multi-purpose use, including as a base class for specific factories.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    public class GenericFactory<TOption, TItem> : IFactory<TOption, TItem>
        where TOption : class
    {
        private readonly IServiceProvider services;
        private readonly IOptionsMonitor<TOption> options;
        private readonly IOptions<GenericFactoryOptions<TOption, TItem>>? factoryOptions;
        private readonly ConcurrentDictionary<string, Result<TItem>> items = new ConcurrentDictionary<string, Result<TItem>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFactory{TOption, TItem}"/> class.
        /// </summary>
        /// <param name="services">The IServiceProvider.</param>
        /// <param name="options">The IOptionsMonitor for the item options.</param>
        /// <param name="factoryOptions">The IOpctions for configuring the factory.</param>
        public GenericFactory(IServiceProvider services, IOptionsMonitor<TOption> options, IOptions<GenericFactoryOptions<TOption, TItem>> factoryOptions)
        {
            this.services = services;
            this.options = options;
            this.factoryOptions = factoryOptions;

            if (this.factoryOptions?.Value?.PrefabricatedItems != null)
            {
                foreach (KeyValuePair<string, TItem> item in this.factoryOptions.Value.PrefabricatedItems)
                {
                    _ = this.items.TryAdd(item.Key, item.Value); // Will always suceed.
                }
            }
        }

        /// <inheritdoc/>
        public Result<TItem> GetItem(string name)
        {
            return this.items.GetOrAdd(name, n =>
            {
                if (this.factoryOptions?.Value?.CreateDefaultItem == null)
                {
                    return new NoFactoryItemGeneratorDefinedError(typeof(TOption), typeof(TItem), n);
                }
                else
                {
                    TOption o = this.options.Get(n);

                    if (o == null && this.factoryOptions.Value.AllowMissingOptions == false)
                    {
                        return new MissingOptionsError(typeof(TOption), typeof(TItem), n);
                    }

                    return this.factoryOptions.Value.CreateDefaultItem.Invoke(this.services, o);
                }
            });
        }
    }
}
