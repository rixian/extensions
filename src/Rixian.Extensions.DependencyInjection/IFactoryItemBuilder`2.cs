// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines a builder for configuring factory items.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    public interface IFactoryItemBuilder<TOption, TItem>
        where TOption : class
    {
        /// <summary>
        /// Gets the IServiceCollection that maintains the factory configuration and services.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
