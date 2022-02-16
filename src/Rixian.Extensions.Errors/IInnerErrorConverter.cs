// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Errors
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Converter for IInnerError interfaces.
    /// </summary>
    public class IInnerErrorConverter : JsonConverter<IInnerError>
    {
        /// <inheritdoc/>
        public override IInnerError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<Error>(ref reader, options);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, IInnerError value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
