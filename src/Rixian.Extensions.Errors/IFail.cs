// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    /// <summary>
    /// Represents a result that is a fail.
    /// </summary>
    public interface IFail
    {
        /// <summary>
        /// Gets the error.
        /// </summary>
        Error Error { get; }
    }
}
