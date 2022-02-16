// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Converter for an IEnumerable of type IError.
    /// </summary>
    public class ErrorEnumerableConverter : JsonConverter<IEnumerable<IError>>
    {
        /// <inheritdoc/>
        public override IEnumerable<IError>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<List<Error>>(ref reader, options);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, IEnumerable<IError> value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartArray();
            foreach (IError? error in value)
            {
                JsonSerializer.Serialize(writer, error, error.GetType(), options);
            }

            writer.WriteEndArray();
        }
    }
}
