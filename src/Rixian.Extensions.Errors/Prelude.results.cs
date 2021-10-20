// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    /// <summary>
    /// Provides helper methods for working with Errors and Results.
    /// </summary>
    public static partial class Prelude
    {
        /// <summary>
        /// The default result with no error.
        /// </summary>
        public static readonly Result DefaultResult = Rixian.Extensions.Errors.Result.Default;

        /// <summary>
        /// Creates a result with a value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="item">The value to wrap in the result.</param>
        /// <returns>The result.</returns>
        public static Result<T> Result<T>(T item)
        {
            return Rixian.Extensions.Errors.Result.New(item);
        }

        /// <summary>
        /// Creates a result with a null value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The result.</returns>
        public static Result<T> NullResult<T>()
            where T : class
        {
            return Rixian.Extensions.Errors.Result.Null<T>();
        }

        /// <summary>
        /// Creates a result with an error.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="innerError">The error.</param>
        /// <returns>The result.</returns>
        public static Result<T> Error<T>(Error innerError) => Rixian.Extensions.Errors.Result.New<T>(innerError);

        /// <summary>
        /// Creates a result with an error.
        /// </summary>
        /// <param name="innerError">The error.</param>
        /// <returns>The result.</returns>
        public static Result Error(Error innerError) => Rixian.Extensions.Errors.Result.New(innerError);
    }
}
