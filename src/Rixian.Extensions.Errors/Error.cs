// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The base error object.
    /// </summary>
#pragma warning disable CA1716 // Identifiers should not match keywords
    public record Error : IInnerError, IError
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new IInnerErrorConverter(),
                new IErrorConverter(),
                new ErrorEnumerableConverter(),
            },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        public Error()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        public Error(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public Error(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="target">The target of the error.</param>
        public Error(string code, string message, string target)
        {
            this.Code = code;
            this.Message = message;
            this.Target = target;
        }

        /// <inheritdoc/>
        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? Code { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("innererror")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(IInnerErrorConverter))]
        public IInnerError? InnerError { get; set; }

        /// <inheritdoc/>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("target")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ErrorEnumerableConverter))]
        public IEnumerable<IError>? Details { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IInnerError> Flatten()
        {
            yield return this;

            if (this.InnerError is not null)
            {
                foreach (IInnerError? error in this.InnerError.Flatten())
                {
                    yield return error;
                }
            }

            yield break;
        }

        /// <inheritdoc/>
        public string ToJson(JsonSerializerOptions? jsonSerializerOptions = default)
        {
            return JsonSerializer.Serialize(this, this.GetType(), jsonSerializerOptions ?? Error.SerializerOptions);
        }

        /// <summary>
        /// Deserializes the JSON into an error object using the built in options.
        /// </summary>
        /// <param name="json">The json input to deserialize.</param>
        /// <param name="jsonSerializerOptions">Optional JsonSerializerOptions to use.</param>
        /// <returns>The serialized JSON.</returns>
        public static Error? FromJson(string json, JsonSerializerOptions? jsonSerializerOptions = default)
        {
            return FromJson<Error>(json, jsonSerializerOptions);
        }

        /// <summary>
        /// Deserializes the JSON into an error object using the built in options.
        /// </summary>
        /// <typeparam name="TError">The custom error type.</typeparam>
        /// <param name="json">The json input to deserialize.</param>
        /// <param name="jsonSerializerOptions">Optional JsonSerializerOptions to use.</param>
        /// <returns>The serialized JSON.</returns>
        public static TError? FromJson<TError>(string json, JsonSerializerOptions? jsonSerializerOptions = default)
            where TError : IError
        {
            return JsonSerializer.Deserialize<TError>(json, jsonSerializerOptions ?? Error.SerializerOptions);
        }
    }
}
