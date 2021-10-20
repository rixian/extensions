// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The base error object.
    /// </summary>
#pragma warning disable CA1716 // Identifiers should not match keywords
    public record Error
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        /// <summary>
        /// Gets or sets the server-defined error codes.
        /// </summary>
        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the human-readable error message.
        /// </summary>
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error target.
        /// </summary>
        [JsonPropertyName("target")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; set; }

        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<Error>? Details { get; set; }

        /// <summary>
        /// Gets or sets the inner error with more detailed information.
        /// </summary>
        [JsonPropertyName("innererror")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Error? InnerError { get; set; }
    }
}
