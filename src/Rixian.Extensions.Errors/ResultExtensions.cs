// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System;

/// <summary>
/// Extensions for working with the Result classes and interfaces.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Casts the current Result instance to an instance of Fail.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>The Fail instance.</returns>
    public static Fail AsFail(this Result result)
    {
        return (Fail)result;
    }

    /// <summary>
    /// Casts the current Result instance to an instance of Fail.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>The Fail instance.</returns>
    public static IFail AsFail(this IResult result)
    {
        if (result is IFail fail)
        {
            return fail;
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Casts the current Result instance to an instance of Success.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>The Success instance.</returns>
    public static Success<T> AsSuccess<T>(this Result result)
    {
        return (Success<T>)result;
    }

    /// <summary>
    /// Casts the current Result instance to an instance of Success.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>The Success instance.</returns>
    public static ISuccess<T> AsSuccess<T>(this IResult result)
    {
        if (result is ISuccess<T> success)
        {
            return success;
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Ensures that there is a value. Throws if there is not.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <exception cref="ErrorException">The exception containing the error.</exception>
    public static void EnsureSuccess(this IResult result)
    {
        if (result.IsSuccess is false)
        {
            IFail fail = result.AsFail();
            throw new ErrorException(fail.Error, fail.Error.Message ?? string.Empty);
        }
    }

    /// <summary>
    /// Retrieves the value. Throws if it is not successful..
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <exception cref="ErrorException">The exception containing the error.</exception>
    /// <returns>The value.</returns>
    public static T? GetValueOrThrow<T>(this IResult result)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (result is ISuccess<T> successResult)
        {
            return successResult.Value;
        }
        else if (result is IFail failResult)
        {
            throw new ErrorException(failResult.Error, failResult.Error.Message ?? string.Empty);
        }
        else
        {
            throw new NotSupportedException("Result is in an impossible state.");
        }
    }

    /// <summary>
    /// Performs a mapping depending on the type of the stored value.
    /// </summary>
    /// <typeparam name="TResult">The resultant type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onSuccess">The mapping for a value.</param>
    /// <param name="onError">The mapping for an error.</param>
    /// <returns>The mapped result.</returns>
    public static TResult Match<TResult>(this IResult result, Func<TResult> onSuccess, Func<Error, TResult> onError)
    {
        if (result.IsSuccess && onSuccess is not null)
        {
            return onSuccess();
        }

        return result.IsSuccess switch
        {
            false when onError is not null => onError(result.AsFail().Error),
            _ => throw new InvalidOperationException(),
        };
    }

    /// <summary>
    /// Performs a mapping depending on the type of the stored value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TResult">The resultant type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onSuccess">The mapping for a value.</param>
    /// <param name="onError">The mapping for an error.</param>
    /// <returns>The mapped result.</returns>
    public static TResult Match<TValue, TResult>(this IResult result, Func<TValue?, TResult> onSuccess, Func<Error, TResult> onError)
    {
        if (result.IsSuccess && onSuccess is not null)
        {
            return onSuccess(result.AsSuccess<TValue>().Value);
        }

        return result.IsSuccess switch
        {
            false when onError is not null => onError(result.AsFail().Error),
            _ => throw new InvalidOperationException(),
        };
    }

    /// <summary>
    /// Executes one of the actions depending on the type of the stored value.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="onSuccess">The action to execute for a value.</param>
    /// <param name="onError">The action to execute for an error.</param>
    public static void Switch(this IResult result, Action onSuccess, Action<Error> onError)
    {
        if (result.IsSuccess && onSuccess is not null)
        {
            onSuccess();
            return;
        }

        if (result.IsSuccess is false && onError is not null)
        {
            onError(result.AsFail().Error);
            return;
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Executes one of the actions depending on the type of the stored value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onSuccess">The action to execute for a value.</param>
    /// <param name="onError">The action to execute for an error.</param>
    public static void Switch<TValue>(this IResult result, Action<TValue?> onSuccess, Action<Error> onError)
    {
        if (result.IsSuccess && onSuccess is not null)
        {
            onSuccess(result.AsSuccess<TValue>().Value);
            return;
        }

        if (result.IsSuccess is false && onError is not null)
        {
            onError(result.AsFail().Error);
            return;
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Trys to get the value from the result instance.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="value">The result value.</param>
    /// <returns>A flag indicating success.</returns>
    public static bool TryGetValue<T>(this Result<T> result, out T value)
    {
        if (result.IsSuccess)
        {
            value = result.Value;
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    /// <summary>
    /// Trys to get the value from the result instance.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="value">The result value.</param>
    /// <returns>A flag indicating success.</returns>
    public static bool TryGetValue<T>(this Result result, out T value)
    {
        if (result.IsSuccess && result is Success<T> success)
        {
            value = success.Value;
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }
}
