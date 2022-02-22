// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors;

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the innererror object in the specs.
/// </summary>
public interface IInnerError
{
    /// <summary>
    /// Gets or sets the server-defined error codes.
    /// </summary>
    /// <remarks>
    /// The value for the "code" name/value pair is a language-independent string. Its value is a service-defined error code
    /// that SHOULD be human-readable. This code serves as a more specific indicator of the error than the HTTP error code
    /// specified in the response. Services SHOULD have a relatively small number (about 20) of possible values for "code,"
    /// and all clients MUST be capable of handling all of them. Most services will require a much larger number of more
    /// specific error codes, which are not interesting to all clients. These error codes SHOULD be exposed in the "innererror"
    /// name/value pair as described below. Introducing a new value for "code" that is visible to existing clients is a breaking
    /// change and requires a version increase. Services can avoid breaking changes by adding new error codes to "innererror" instead.
    ///
    /// Per the spec (https://github.com/microsoft/api-guidelines/blob/42372903dbdb50ac25e3634cce01ce6441bf2b61/Guidelines.md).
    /// </remarks>
    [JsonPropertyName("code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    string? Code { get; set; }

    /// <summary>
    /// Gets or sets the extension data values.
    /// </summary>
    [JsonExtensionData]
    Dictionary<string, JsonElement>? ExtensionData { get; set; }

    /// <summary>
    /// Gets or sets the inner error with more detailed information.
    /// </summary>
    /// <remarks>
    /// The value for the "innererror" name/value pair MUST be an object. The contents of this object are service-defined.
    /// Services wanting to return more specific errors than the root-level code MUST do so by including a name/value pair
    /// for "code" and a nested "innererror." Each nested "innererror" object represents a higher level of detail than its parent.
    /// When evaluating errors, clients MUST traverse through all of the nested "innererrors" and choose the deepest one
    /// that they understand. This scheme allows services to introduce new error codes anywhere in the hierarchy without
    /// breaking backwards compatibility, so long as old error codes still appear. The service MAY return different levels
    /// of depth and detail to different callers. For example, in development environments, the deepest "innererror" MAY
    /// contain internal information that can help debug the service. To guard against potential security concerns around
    /// information disclosure, services SHOULD take care not to expose too much detail unintentionally. Error objects MAY
    /// also include custom server-defined name/value pairs that MAY be specific to the code. Error types with custom
    /// server-defined properties SHOULD be declared in the service's metadata document
    ///
    /// Per the spec (https://github.com/microsoft/api-guidelines/blob/42372903dbdb50ac25e3634cce01ce6441bf2b61/Guidelines.md).
    /// </remarks>
    [JsonPropertyName("innererror")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IInnerError? InnerError { get; set; }

    /// <summary>
    /// Flattens the inner errors into a single enumerable.
    /// </summary>
    /// <returns>The flattened error collection.</returns>
    IEnumerable<IInnerError> Flatten();

    /// <summary>
    /// Serializes the current error object to JSON using the built in options.
    /// </summary>
    /// <param name="jsonSerializerOptions">Optional JsonSerializerOptions to use.</param>
    /// <returns>The serialized JSON.</returns>
    string ToJson(JsonSerializerOptions? jsonSerializerOptions = null);
}
