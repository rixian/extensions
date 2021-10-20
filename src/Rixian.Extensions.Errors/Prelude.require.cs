// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System;

    /// <summary>
    /// Provides helper methods for working with Errors and Results.
    /// </summary>
    public static partial class Prelude
    {
        /// <summary>
        /// Requires a non-empty GUID.
        /// </summary>
        /// <param name="argument">The GUID to validate.</param>
        /// <returns>A Result with an error if the argument is empty.</returns>
        public static Result RequireGuid(Guid? argument)
        {
            if (argument.HasValue == false)
            {
                return NullValueDisallowedError().ToResult();
            }

            return RequireGuid(argument.Value);
        }

        /// <summary>
        /// Requires a non-empty GUID.
        /// </summary>
        /// <param name="argument">The GUID to validate.</param>
        /// <returns>A Result with an error if the argument is empty.</returns>
        public static Result RequireGuid(Guid argument)
        {
            if (argument == Guid.Empty)
            {
                return NullValueDisallowedError().ToResult();
            }

            return DefaultResult;
        }
    }
}
