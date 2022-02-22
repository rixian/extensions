// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection;

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Rixian.Extensions.DependencyInjection;
using Rixian.Extensions.Errors;

/// <summary>
/// Extension methods for working with the IFactory interface.
/// </summary>
public static class FactoryExtensions
{
    /// <summary>
    /// Configures an existing factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="services">The IServiceCollection.</param>
    /// <returns>The IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> ConfigureFactory<TOption, TItem>(this IServiceCollection services)
        where TOption : class
    {
        services.Configure<GenericFactoryOptions<TOption, TItem>>(o =>
        {
            o.CreateDefaultItem = null;
        });
        return new GenericFactoryItemBuilder<TOption, TItem>(services);
    }

    /// <summary>
    /// Configures an existing factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="services">The IServiceCollection.</param>
    /// <param name="createDefaultItem">Method for creating new items for keys that don't exist.</param>
    /// <returns>The IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> ConfigureFactory<TOption, TItem>(this IServiceCollection services, Func<IServiceProvider, TOption?, TItem> createDefaultItem)
        where TOption : class
    {
        services.Configure<GenericFactoryOptions<TOption, TItem>>(o =>
        {
            o.CreateDefaultItem = createDefaultItem;
        });
        return new GenericFactoryItemBuilder<TOption, TItem>(services);
    }

    /// <summary>
    /// Adds a new generic factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="services">The IServiceCollection.</param>
    /// <returns>The IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> AddFactory<TOption, TItem>(this IServiceCollection services)
        where TOption : class
    {
        services.TryAddSingleton<IFactory<TOption, TItem>, GenericFactory<TOption, TItem>>();
        return services.ConfigureFactory<TOption, TItem>();
    }

    /// <summary>
    /// Adds a new generic factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="services">The IServiceCollection.</param>
    /// <param name="createDefaultItem">Method for creating new items for keys that don't exist.</param>
    /// <returns>The IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> AddFactory<TOption, TItem>(this IServiceCollection services, Func<IServiceProvider, TOption?, TItem> createDefaultItem)
        where TOption : class
    {
        services.TryAddSingleton<IFactory<TOption, TItem>, GenericFactory<TOption, TItem>>();
        return services.ConfigureFactory<TOption, TItem>(createDefaultItem);
    }

    /// <summary>
    /// Configures a new item in the factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="builder">The IFactoryItemBuilder.</param>
    /// <param name="configureOptions">Method for configuring the default item options.</param>
    /// <returns>The updated IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> Configure<TOption, TItem>(this IFactoryItemBuilder<TOption, TItem> builder, Action<TOption> configureOptions)
        where TOption : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.Configure<TOption>(configureOptions);
        return builder;
    }

    /// <summary>
    /// Configures a new item in the factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="builder">The IFactoryItemBuilder.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="configureOptions">Method for configuring the named item options.</param>
    /// <returns>The updated IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> Configure<TOption, TItem>(this IFactoryItemBuilder<TOption, TItem> builder, string name, Action<TOption> configureOptions)
        where TOption : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.Configure<TOption>(name, configureOptions);
        return builder;
    }

    /// <summary>
    /// Configures a new item in the factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <typeparam name="TBuilder">The type of item builder.</typeparam>
    /// <param name="builder">The IFactoryItemBuilder.</param>
    /// <param name="configureOptions">Method for configuring the named item options.</param>
    /// <returns>The updated IFactoryItemBuilder.</returns>
    public static TBuilder Configure<TOption, TItem, TBuilder>(this TBuilder builder, Action<TOption> configureOptions)
        where TBuilder : IFactoryItemBuilder<TOption, TItem>
        where TOption : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.Configure<TOption>(configureOptions);
        return builder;
    }

    /// <summary>
    /// Configures a new item in the factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <typeparam name="TBuilder">The type of item builder.</typeparam>
    /// <param name="builder">The IFactoryItemBuilder.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="configureOptions">Method for configuring the named item options.</param>
    /// <returns>The updated IFactoryItemBuilder.</returns>
    public static TBuilder Configure<TOption, TItem, TBuilder>(this TBuilder builder, string name, Action<TOption> configureOptions)
        where TBuilder : IFactoryItemBuilder<TOption, TItem>
        where TOption : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.Configure<TOption>(name, configureOptions);
        return builder;
    }

    /// <summary>
    /// Registers a default item with no specified name.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="builder">The IFactoryItemBuilder.</param>
    /// <param name="item">The item to add.</param>
    /// <returns>The updated IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> AddDefaultItem<TOption, TItem>(this IFactoryItemBuilder<TOption, TItem> builder, TItem item)
        where TOption : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.Configure<GenericFactoryOptions<TOption, TItem>>(o =>
        {
            o.PrefabricatedItems[Options.DefaultName] = item;
        });
        return builder;
    }

    /// <summary>
    /// Registers an item with a specified name.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="builder">The IFactoryItemBuilder.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="item">The item to add.</param>
    /// <returns>The updated IFactoryItemBuilder.</returns>
    public static IFactoryItemBuilder<TOption, TItem> AddItem<TOption, TItem>(this IFactoryItemBuilder<TOption, TItem> builder, string name, TItem item)
        where TOption : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.Configure<GenericFactoryOptions<TOption, TItem>>(o =>
        {
            o.PrefabricatedItems[name] = item;
        });
        return builder;
    }

    /// <summary>
    /// Gets the default item from the factory.
    /// </summary>
    /// <typeparam name="TOption">The type of options used.</typeparam>
    /// <typeparam name="TItem">The type of item produced.</typeparam>
    /// <param name="factory">The IFactory.</param>
    /// <returns>A result containing the item or an error.</returns>
    public static Result<TItem> GetItem<TOption, TItem>(this IFactory<TOption, TItem> factory)
        where TOption : class
    {
        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        return factory.GetItem(Options.DefaultName);
    }
}
