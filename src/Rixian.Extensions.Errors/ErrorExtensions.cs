// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    /// <summary>
    /// Extension methods for working with error objects.
    /// </summary>
    public static class ErrorExtensions
    {
        /// <summary>
        /// Turns the error into a result.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="innerError">The error.</param>
        /// <returns>The result.</returns>
        public static Result<T> ToResult<T>(this Error innerError)
        {
            return Result.New<T>(innerError);
        }

        /// <summary>
        /// Turns the error into a result.
        /// </summary>
        /// <param name="innerError">The error.</param>
        /// <returns>The result.</returns>
        public static Result ToResult(this Error innerError)
        {
            return Result.New(innerError);
        }

        /// <summary>
        /// Creates an <see cref="ErrorResponse"/> from this Error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The <see cref="ErrorResponse"/>.</returns>
        public static ErrorResponse ToResponse(this Error error)
        {
            return new ErrorResponse(error);
        }

        /// <summary>
        /// Creates an <see cref="ErrorResponse"/> from this result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The <see cref="ErrorResponse"/>.</returns>
        public static ErrorResponse? ToErrorResponse(this Result result)
        {
            if (result is null)
            {
                throw new System.ArgumentNullException(nameof(result));
            }

            return result.AsFail().Error?.ToResponse();
        }

        /// <summary>
        /// Converts a failed result to different type.
        /// </summary>
        /// <typeparam name="T">The new type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>The new result.</returns>
        public static Result<T> Cast<T>(this Result result)
        {
            return (Result<T>)result;
        }
    }
}
