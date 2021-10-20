// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Builder class for configuring factory items.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    internal class GenericFactoryItemBuilder<TOption, TItem> : IFactoryItemBuilder<TOption, TItem>
        where TOption : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFactoryItemBuilder{TOption, TItem}"/> class.
        /// </summary>
        /// <param name="services">The IServiceCollection.</param>
        public GenericFactoryItemBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        /// <inheritdoc/>
        public IServiceCollection Services { get; }
    }
}
