// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license.

#nullable enable
namespace Rixian.Extensions.AspNetCore
{
    using System.Collections.Generic;
    using Rixian.Extensions.Errors;
    using static Rixian.Extensions.Errors.Prelude;

    /// <summary>
    /// Configuration class for the OpenID Connect configuration.
    /// </summary>
    public class OAuth2Config
    {
        /// <summary>
        /// Gets or sets the OpenID Connect authority.
        /// </summary>
        public string? Authority { get; set; }

        /// <summary>
        /// Gets or sets the optional custom health endpoint.
        /// </summary>
        public string? AuthorityHealthEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the api name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the api secret.
        /// </summary>
        public string? Secret { get; set; }

        /// <summary>
        /// Checks if all the required configuration values are present.
        /// </summary>
        /// <returns>An optional error result or nothing.</returns>
        public Result CheckRequiredValues()
        {
            List<Error>? errors = null;

            if (string.IsNullOrWhiteSpace(this.Authority))
            {
                errors ??= new List<Error>();
                errors.Add(new MissingRequiredConfigurationFieldError(nameof(this.Authority)));
            }


            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors ??= new List<Error>();
                errors.Add(new MissingRequiredConfigurationFieldError(nameof(this.Name)));
            }

            if (errors != null)
            {
                return ErrorResult(new InvalidConfigurationError
                {
                    Details = errors,
                });
            }

            return DefaultResult;
        }

        /// <summary>
        /// Ensures that all the required configuration values are present. Throws an <see cref="ErrorException"/> if not.
        /// </summary>
        public void EnsureRequiredValues()
        {
            Result isValid = this.CheckRequiredValues();
            if (isValid.IsFail)
            {
                throw new ErrorException(isValid.Error);
            }
        }
    }
}
