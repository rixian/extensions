// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System;

/// <summary>
/// Http problem type as an error, specifically "application/problem+json".
/// See: https://tools.ietf.org/html/rfc7807 for details.
/// </summary>
public record HttpProblemError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpProblemError"/> class.
    /// </summary>
    /// <param name="problem">The HttpProblem.</param>
    public HttpProblemError(HttpProblem problem)
    {
        if (problem is null)
        {
            throw new ArgumentNullException(nameof(problem));
        }

        this.Code = problem.Type;
        this.Message = string.Join(Environment.NewLine, problem.Title, problem.Detail);
        this.Status = problem.Status;
        this.Instance = problem.Instance;
        this.Problem = problem;
    }

    /// <summary>
    /// Gets or sets the problem status.
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Gets or sets the problem instance.
    /// </summary>
    public string? Instance { get; set; }

    /// <summary>
    /// Gets or sets the problem.
    /// </summary>
    public HttpProblem Problem { get; set; }
}
