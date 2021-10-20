// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System;

    /// <summary>
    /// Extensions for working with tuples for errors.
    /// </summary>
    public static class TupleExtensions
    {
        /// <summary>
        /// Retrieves the value. Throws if it is not successful..
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tuple">The tuple.</param>
        /// <exception cref="ErrorException">The exception containing the error.</exception>
        /// <returns>The value.</returns>
        public static T? GetValueOrThrow<T>(this (T? Value, Error? Err) tuple)
        {
            if (tuple.Err == null)
            {
                return tuple.Value;
            }
            else
            {
                throw new ErrorException(tuple.Err, tuple.Err.Message ?? string.Empty);
            }
        }

        /// <summary>
        /// Ensures that there is a value. Throws if there is not.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="tuple">The tuple.</param>
        /// <exception cref="ErrorException">The exception containing the error.</exception>
        public static void EnsureSuccess<T>(this (T? Value, Error? Err) tuple)
        {
            if (tuple.Err == null)
            {
                return;
            }
            else
            {
                throw new ErrorException(tuple.Err, tuple.Err.Message ?? string.Empty);
            }
        }

        /// <summary>
        /// Executes one of the actions depending on the type of the stored value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="tuple">The tuple.</param>
        /// <param name="onSuccess">The action to execute for a value.</param>
        /// <param name="onError">The action to execute for an error.</param>
        public static void Switch<TValue>(this (TValue? Value, Error? Err) tuple, Action<TValue?> onSuccess, Action<Error> onError)
        {
            if (tuple.Err == null && onSuccess != null)
            {
                onSuccess(tuple.Value);
                return;
            }

            if (tuple.Err != null && onError != null)
            {
                onError(tuple.Err);
                return;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Performs a mapping depending on the type of the stored value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TResult">The resultant type.</typeparam>
        /// <param name="tuple">The tuple.</param>
        /// <param name="onSuccess">The mapping for a value.</param>
        /// <param name="onError">The mapping for an error.</param>
        /// <returns>The mapped result.</returns>
        public static TResult Match<TValue, TResult>(this (TValue? Value, Error? Err) tuple, Func<TValue?, TResult> onSuccess, Func<Error, TResult> onError)
        {
            if (tuple.Err == null && onSuccess != null)
            {
                return onSuccess(tuple.Value);
            }

            if (tuple.Err != null && onError != null)
            {
                return onError(tuple.Err);
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Trys to get the value from the tuple.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="tuple">The tuple.</param>
        /// <param name="value">The result value.</param>
        /// <returns>A flag indicating success.</returns>
        public static bool TryGetValue<TValue>(this (TValue? Value, Error? Err) tuple, out TValue? value)
        {
            if (tuple.Err is null)
            {
                value = default;
                return false;
            }
            else
            {
                value = tuple.Value;
                return true;
            }
        }

        /// <summary>
        /// Converts the tuple type to a non-null value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="tuple">The tuple.</param>
        /// <returns>The updated tuple.</returns>
        public static (TValue Value, Error? Err) AsNotNull<TValue>(this (TValue? Value, Error? Err) tuple)
        {
            tuple.EnsureSuccess();
            return (tuple.Value!, default);
        }
    }
}
