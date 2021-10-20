// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using Rixian.Extensions.Errors;

    /// <summary>
    /// Defines a factory that produces items.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    public interface IFactory<TOption, TItem>
        where TOption : class
    {
        /// <summary>
        /// Gets an item from the factory.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns>A result with either the item or an error.</returns>
        Result<TItem> GetItem(string name);
    }
}
