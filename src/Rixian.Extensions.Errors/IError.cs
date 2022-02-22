// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The base error object.
    /// </summary>
    public interface IError : IInnerError
    {
        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        /// <remarks>
        /// The value for the "details" name/value pair MUST be an array of JSON objects that MUST contain name/value pairs for "code"
        /// and "message," and MAY contain a name/value pair for "target," as described above. The objects in the "details" array usually
        /// represent distinct, related errors that occurred during the request.
        ///
        /// Per the spec (https://github.com/microsoft/api-guidelines/blob/42372903dbdb50ac25e3634cce01ce6441bf2b61/Guidelines.md).
        /// </remarks>
        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        IEnumerable<IError>? Details { get; set; }

        /// <summary>
        /// Gets or sets the human-readable error message.
        /// </summary>
        /// <remarks>
        /// The value for the "message" name/value pair MUST be a human-readable representation of the error. It is intended as an aid to
        /// developers and is not suitable for exposure to end users. Services wanting to expose a suitable message for end users MUST do
        /// so through an annotation or custom property. Services SHOULD NOT localize "message" for the end user, because doing so might
        /// make the value unreadable to the app developer who may be logging the value, as well as make the value less searchable on the Internet.
        ///
        /// Per the spec (https://github.com/microsoft/api-guidelines/blob/42372903dbdb50ac25e3634cce01ce6441bf2b61/Guidelines.md).
        /// </remarks>
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error target.
        /// </summary>
        /// <remarks>
        /// The value for the "target" name/value pair is the target of the particular error (e.g., the name of the property in error).
        ///
        /// Per the spec (https://github.com/microsoft/api-guidelines/blob/42372903dbdb50ac25e3634cce01ce6441bf2b61/Guidelines.md).
        /// </remarks>
        [JsonPropertyName("target")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? Target { get; set; }
    }
}
