// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Caching.Tests;

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Rixian.Extensions.Errors;
using Xunit;
using Xunit.Abstractions;

public class ErrorTests
{
    private readonly ITestOutputHelper logger;

    public ErrorTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Fact]
    public void Serialize_BasicError_ToJson_Success()
    {
        var err = new Error
        {
            Code = "test_error",
            Message = "This is a test.",
        };
        var json = err.ToJson();
        this.logger.WriteLine(json);

        JsonElement jobj = JsonSerializer.Deserialize<JsonElement>(json);
        jobj.ValueKind.Should().Be(JsonValueKind.Object);
    }

    [Fact]
    public void Serialize_ContactInfoError_DefaultSerialize_Success()
    {
        Error? err = TestErrors.ContactInfoError;

        var json = JsonSerializer.Serialize(err);
        this.logger.WriteLine(json);

        JsonElement jobj = JsonSerializer.Deserialize<JsonElement>(json);
        jobj.ValueKind.Should().Be(JsonValueKind.Object);
    }

    [Fact]
    public void Serialize_ContactInfoError_ToJson_Success()
    {
        Error? err = TestErrors.ContactInfoError;

        var json = err.ToJson();
        this.logger.WriteLine(json);

        JsonElement jobj = JsonSerializer.Deserialize<JsonElement>(json);
        jobj.ValueKind.Should().Be(JsonValueKind.Object);
    }

    [Fact]
    public void Serialize_PasswordError_DefaultSerialize_Success()
    {
        Error? err = TestErrors.PasswordError;
        var json = JsonSerializer.Serialize(err);
        this.logger.WriteLine(json);

        JsonElement jobj = JsonSerializer.Deserialize<JsonElement>(json);
        jobj.ValueKind.Should().Be(JsonValueKind.Object);
    }

    [Fact]
    public void Serialize_PasswordError_ToJson_Success()
    {
        Error? err = TestErrors.PasswordError;
        var json = err.ToJson();
        this.logger.WriteLine(json);

        JsonElement jobj = JsonSerializer.Deserialize<JsonElement>(json);
        jobj.ValueKind.Should().Be(JsonValueKind.Object);
    }

    /// <summary>
    /// Errors for use in unit tests.
    /// </summary>
    public static class TestErrors
    {
        public static readonly Error ContactInfoError = new Error
        {
            Code = "BadArgument",
            Message = "Multiple errors in ContactInfo data",
            Target = "ContactInfo",
            Details = new List<IError>
            {
                new Error("NullValue", "Phone number must not be null", "PhoneNumber"),
                new Error("NullValue", "Last name must not be null", "LastName"),
                new Error("MalformedValue", "Address is not valid", "Address"),
            },
        };

        public static readonly Error PasswordError = new Error
        {
            Code = "BadArgument",
            Message = "Previous passwords may not be reused",
            Target = "password",
            InnerError = new Error("PasswordError")
            {
                InnerError = new PasswordPolicyError("PasswordDoesNotMeetPolicy")
                {
                    MinLength = 6,
                    MaxLength = 64,
                    MinDistinctCharacterTypes = 2,
                    CharacterTypes = new List<string> { "lowerCase", "upperCase", "number", "symbol" },
                    InnerError = new Error("PasswordReuseNotAllowed"),
                },
            },
        };

        public record PasswordPolicyError : Error
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PasswordPolicyError"/> class.
            /// </summary>
            /// <param name="code">The error code.</param>
            public PasswordPolicyError(string code)
                : base(code)
            {
            }

            [JsonPropertyName("minLength")]
            public int MinLength { get; set; }

            [JsonPropertyName("maxLength")]
            public int MaxLength { get; set; }

            [JsonPropertyName("minDistinctCharacterTypes")]
            public int MinDistinctCharacterTypes { get; set; }

            [JsonPropertyName("characterTypes")]
            public List<string> CharacterTypes { get; set; } = new List<string>();
        }
    }
}
