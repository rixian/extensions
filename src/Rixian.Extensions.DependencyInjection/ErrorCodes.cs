// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.DependencyInjection
{
    /// <summary>
    /// Error codes for errors created by this library.
    /// </summary>
    public static class ErrorCodes
    {
        /// <summary>
        /// Code used when there is no default factory item generator defined.
        /// </summary>
        public static readonly string NoFactoryItemGeneratorDefined = "no_factory_item_generator_defined";

        /// <summary>
        /// Code used when there is no named option defined.
        /// </summary>
        public static readonly string MissingFactoryItemOptions = "missing_factory_item_options";
    }
}
