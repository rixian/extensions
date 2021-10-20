// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    using System;
    using Rixian.Extensions.Errors;

    /// <summary>
    /// An error used when there is no named option defined.
    /// </summary>
    public record MissingOptionsError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingOptionsError"/> class.
        /// </summary>
        /// <param name="optionType">The type of the Option.</param>
        /// <param name="itemType">The type of the Item.</param>
        /// <param name="name">The name of the Item.</param>
        public MissingOptionsError(Type optionType, Type itemType, string name)
        {
            this.Code = ErrorCodes.MissingFactoryItemOptions;
            this.Message = Properties.Resources.MissingFactoryOptionsErrorMessage;
            this.OptionType = optionType;
            this.ItemType = itemType;
            this.Name = name;
        }

        /// <summary>
        /// Gets the type of the Option.
        /// </summary>
        public Type OptionType { get; }

        /// <summary>
        /// Gets the type of the Item.
        /// </summary>
        public Type ItemType { get; }

        /// <summary>
        /// Gets the name of the Item.
        /// </summary>
        public string Name { get; }
    }
}
