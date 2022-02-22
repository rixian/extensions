// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System;

/// <summary>
/// Represents a result that is a fail.
/// </summary>
public sealed record Fail : Result, IFail
{
    private readonly Error error;

    /// <summary>
    /// Initializes a new instance of the <see cref="Fail"/> class.
    /// </summary>
    /// <param name="error">The Error.</param>
    public Fail(Error error)
        : base(isSuccess: false)
    {
        this.error = error;
    }

    /// <inheritdoc/>
    public Error Error
    {
        get
        {
            if (this.IsSuccess)
            {
                throw new InvalidOperationException("InvalidCastToErrorErrorMessage");
            }

            if (this.error is null)
            {
                throw new System.NotSupportedException("Result is in an impossible state.");
            }

            return this.error;
        }
    }

    /// <summary>
    /// Converts an Error to a Fail.
    /// </summary>
    /// <param name="error">The Error.</param>
    public static implicit operator Fail(Error error) => new Fail(error);
}
