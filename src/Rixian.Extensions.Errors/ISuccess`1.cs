// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

/// <summary>
/// Represents a result that is a value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public interface ISuccess<T>
{
    /// <summary>
    /// Gets the result value.
    /// </summary>
    T? Value { get; }
}
