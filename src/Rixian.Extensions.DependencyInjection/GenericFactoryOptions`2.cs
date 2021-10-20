// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Options for configuring the GenericFactory class.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    public class GenericFactoryOptions<TOption, TItem>
        where TOption : class
    {
        /// <summary>
        /// Gets or sets a value indicating whether missing/null options are allowed when creating a new item.
        /// </summary>
        public bool AllowMissingOptions { get; set; } = true;

        /// <summary>
        /// Gets or sets a method for creating new items for keys that don't exist.
        /// </summary>
        public Func<IServiceProvider, TOption?, TItem>? CreateDefaultItem { get; set; }

        /// <summary>
        /// Gets a dictionary of items that will prepopulate the factory.
        /// </summary>
        public IDictionary<string, TItem> PrefabricatedItems { get; } = new Dictionary<string, TItem>();
    }
}
