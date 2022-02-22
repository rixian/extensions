// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a result that is either a value or an error.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public sealed record Result<T> : Result, ISuccess<T>, IFail
{
    private readonly T? value;
    private readonly Error? error;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">The value to store.</param>
    public Result(T value)
        : base(isSuccess: true)
    {
        this.value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="error">The error to store.</param>
    public Result(Error error)
        : base(isSuccess: false)
    {
        this.error = error;
    }

    /// <summary>
    /// Gets the result value.
    /// </summary>
    public T Value
    {
        [return: MaybeNull]
        get
        {
            return this.IsSuccess switch
            {
                false => throw new InvalidOperationException(Properties.Resources.InvalidCastToValueErrorMessage),
                true => this.value!,
            };
        }
    }

    /// <summary>
    /// Gets the error.
    /// </summary>
    public Error Error
    {
        get
        {
            if (this.IsSuccess)
            {
                throw new InvalidOperationException(Properties.Resources.InvalidCastToErrorErrorMessage);
            }

            if (this.error is null)
            {
                throw new System.NotSupportedException("Result is in an impossible state.");
            }

            return this.error;
        }
    }

    /// <summary>
    /// Converts a value into a Result instance.
    /// </summary>
    /// <param name="t">The value.</param>
    public static implicit operator Result<T>(T t) => new(t);

    /// <summary>
    /// Converts an Error into a Result instance.
    /// </summary>
    /// <param name="error">The error.</param>
    public static implicit operator Result<T>(Error error) => new(error);

    /// <summary>
    /// Converts a Result into a value. Throws if not a success result.
    /// </summary>
    /// <param name="result">The result.</param>
    public static implicit operator T(Result<T> result) => result.Value;

    /// <summary>
    /// Converts a Result into an Error instance. Throws if not a fail result.
    /// </summary>
    /// <param name="result">The Error instance.</param>
    public static implicit operator Error(Result<T> result) => result.Error;

    /// <summary>
    /// Converts a Result into a Fail instance. Throws if not a fail result.
    /// </summary>
    /// <param name="result">The Fail instance.</param>
    public static implicit operator Fail(Result<T> result) => new(result.Error);

    /// <summary>
    /// Converts a Fail into a Result instance.
    /// </summary>
    /// <param name="fail">The Fail instance.</param>
    public static implicit operator Result<T>(Fail fail) => new(fail.Error);

    /// <summary>
    /// Converts a Success into a Result.
    /// </summary>
    /// <param name="success">The Success instance.</param>
    public static implicit operator Result<T>(Success<T> success) => new(success.Value);

    /// <summary>
    /// Converts a Result instance into a Success. Throws if not a success result.
    /// </summary>
    /// <param name="result">The Success instance.</param>
    public static implicit operator Success<T>(Result<T> result) => new(result.Value);

    /// <summary>
    /// Converts a Result instance to a tuple.
    /// </summary>
    /// <param name="result">The Result instance.</param>
    /// <returns>The tuple containing the result values.</returns>
    public static implicit operator (T? Value, Error? Err)(Result<T> result)
    {
        return result.IsSuccess switch
        {
            true => (result.Value, default),
            false => (default, result.Error),
        };
    }

    /// <summary>
    /// Converts a tuple to a Result instance.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>The Result containing the tuple values.</returns>
    public static implicit operator Result<T?>((T? Value, Error? Err) tuple)
    {
        return tuple.Err switch
        {
            null => new Result<T?>(tuple.Value),
            _ => new Result<T?>(tuple.Err),
        };
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.IsSuccess switch
        {
            true => FormatValue(typeof(T), this.value),
            false => FormatValue(typeof(Error), this.error),
        };
    }

    private static string FormatValue<TValue>(Type type, TValue value) => $"{type.FullName}: {value?.ToString()}";
}
