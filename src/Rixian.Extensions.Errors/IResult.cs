// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

/// <summary>
/// Represents a result that is either a success or an error.
/// See: https://stackoverflow.com/a/428ResultType.Success626/664ResultType.Success574.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the value is a result or not.
    /// </summary>
    bool IsSuccess { get; }
}
