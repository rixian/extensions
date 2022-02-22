// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System.Collections.Generic;

/// <summary>
/// Provides helper methods for working with Errors and Results.
/// </summary>
public static partial class Prelude
{
    /// <summary>
    /// Returns the UnhandledError object.
    /// </summary>
    /// <returns>The error.</returns>
    public static Error UnhandledError()
    {
        return Rixian.Extensions.Errors.UnhandledError.Default;
    }

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code) => new Error
    {
        Code = code,
    };

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, Error? innerError) => new Error
    {
        Code = code,
        InnerError = innerError,
    };

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message) => Error(code, message, target: null, details: null, innerError: null);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="target">The target of the error.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, string? target) => Error(code, message, target, details: null, innerError: null);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="target">The target of the error.</param>
    /// <param name="details">More detailed errors.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, string? target, IEnumerable<Error>? details) => Error(code, message, target, details, innerError: null);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="target">The target of the error.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, string? target, Error? innerError) => Error(code, message, target, details: null, innerError);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, Error? innerError) => Error(code, message, target: null, details: null, innerError);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="details">More detailed errors.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, IEnumerable<Error>? details) => Error(code, message, target: null, details, innerError: null);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="details">More detailed errors.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, IEnumerable<Error>? details, Error? innerError) => Error(code, message, target: null, details, innerError);

    /// <summary>
    /// Creates an error object.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <param name="target">The target of the error.</param>
    /// <param name="details">More detailed errors.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error Error(string code, string? message, string? target, IEnumerable<Error>? details, Error? innerError) => new Error
    {
        Code = code,
        Message = message,
        Target = target,
        Details = details,
        InnerError = innerError,
    };

    /// <summary>
    /// Creates an error object for bad arguments.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argumentName">The name of the bad argument.</param>
    /// <returns>The error.</returns>
    public static Error BadArgumentError(string? message, string? argumentName) => Error(ErrorCodes.BadArgument, message, argumentName);

    /// <summary>
    /// Creates an error object for bad arguments.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argumentName">The name of the bad argument.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error BadArgumentError(string? message, string? argumentName, Error? innerError) => Error(ErrorCodes.BadArgument, message, argumentName, innerError);

    /// <summary>
    /// Creates an error object for null arguments.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argumentName">The name of the null argument.</param>
    /// <returns>The error.</returns>
    public static Error NullArgumentDisallowedError(string? message, string? argumentName) => Error(ErrorCodes.NullArgumentDisallowed, message, argumentName);

    /// <summary>
    /// Creates an error object for null arguments.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argumentName">The name of the null argument.</param>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error NullArgumentDisallowedError(string? message, string? argumentName, Error? innerError) => Error(ErrorCodes.NullArgumentDisallowed, message, argumentName, innerError);

    /// <summary>
    /// Creates an error object for disallowed null values.
    /// </summary>
    /// <returns>The error.</returns>
    public static Error NullValueDisallowedError() => Error(ErrorCodes.NullValueDisallowed);

    /// <summary>
    /// Creates an error object for disallowed null values.
    /// </summary>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error NullValueDisallowedError(Error? innerError) => Error(ErrorCodes.NullValueDisallowed, innerError);

    /// <summary>
    /// Creates an error object for disallowed empty GUIDs.
    /// </summary>
    /// <returns>The error.</returns>
    public static Error EmptyGuidDisallowedError() => Error(ErrorCodes.EmptyGuidDisallowed);

    /// <summary>
    /// Creates an error object for disallowed empty GUIDs.
    /// </summary>
    /// <param name="innerError">The inner error.</param>
    /// <returns>The error.</returns>
    public static Error EmptyGuidDisallowedError(Error? innerError) => Error(ErrorCodes.EmptyGuidDisallowed, innerError);
}
