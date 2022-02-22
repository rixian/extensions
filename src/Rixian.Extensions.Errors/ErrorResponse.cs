// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System.Text.Json.Serialization;

/// <summary>
/// The top level error response object.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
    /// </summary>
    public ErrorResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
    /// </summary>
    /// <param name="error">The error to use.</param>
    public ErrorResponse(Error error)
    {
        this.Error = error;
    }

    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Error? Error { get; set; }
}
