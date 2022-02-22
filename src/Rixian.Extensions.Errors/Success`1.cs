// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System;

/// <summary>
/// Represents a result that is a value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public sealed record Success<T> : Result, ISuccess<T>
{
    private readonly T value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Success{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Success(T value)
        : base(isSuccess: true)
    {
        this.value = value;
    }

    /// <inheritdoc/>
    public T Value
    {
        get
        {
            return this.IsSuccess switch
            {
                false => throw new InvalidOperationException("InvalidCastToValueErrorMessage"),
                _ => this.value,
            };
        }
    }

    /// <summary>
    /// Converts a value to a Success.
    /// </summary>
    /// <param name="t">The value.</param>
    public static implicit operator Success<T>(T t) => new Success<T>(t);

    /// <summary>
    /// Converts a Result to a tuple.
    /// </summary>
    /// <param name="success">The Success instance.</param>
    /// <returns>The tuple containing the result values.</returns>
    public static implicit operator (T Value, Error? Err)(Success<T> success) => (success.Value, default);
}
