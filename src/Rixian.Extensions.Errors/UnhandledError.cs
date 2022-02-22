// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

/// <summary>
/// Basic class for unhandled errors.
/// </summary>
public record UnhandledError : Error
{
    /// <summary>
    /// The default UnHandledError.
    /// </summary>
    public static readonly UnhandledError Default = new UnhandledError();

    /// <summary>
    /// Initializes a new instance of the <see cref="UnhandledError"/> class.
    /// </summary>
    public UnhandledError()
    {
        this.Code = "unhandled";
    }
}
