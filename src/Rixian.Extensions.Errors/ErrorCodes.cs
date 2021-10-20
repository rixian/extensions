// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    /// <summary>
    /// Error codes for common errors.
    /// </summary>
    public static class ErrorCodes
    {
        /// <summary>
        /// Error code for bad arguments.
        /// </summary>
        public static readonly string BadArgument = "bad_argument";

        /// <summary>
        /// Error code for disallowed null arguments.
        /// </summary>
        public static readonly string NullArgumentDisallowed = "null_argument_disallowed";

        /// <summary>
        /// Error code for disallowed null values.
        /// </summary>
        public static readonly string NullValueDisallowed = "null_value_disallowed";

        /// <summary>
        /// Error code for disallowed empty guids.
        /// </summary>
        public static readonly string EmptyGuidDisallowed = "empty_guid_disallowed";
    }
}
