// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Extension methods for the ITokenClientBuilder interface.
    /// </summary>
    public static class ClientCredentialTokenClientBuilderExtensions
    {
        /// <summary>
        /// Configures the logical token client with a custom method for getting the HttpClient.
        /// </summary>
        /// <param name="tokenClientBuilder">The ITokenClientBuilder.</param>
        /// <param name="httpClientName">The name of the HttpClient to pull from the IHttpClientFactory.</param>
        /// <returns>The configured ITokenClientBuilder.</returns>
        public static IClientCredentialTokenClientBuilder UseHttpClientForBackchannel(this IClientCredentialTokenClientBuilder tokenClientBuilder, string httpClientName)
        {
            if (tokenClientBuilder is null)
            {
                throw new ArgumentNullException(nameof(tokenClientBuilder));
            }

            tokenClientBuilder.Services.Configure<ClientCredentialsTokenClientOptions>(tokenClientBuilder.Name, o =>
            {
                o.GetBackchannelHttpClient = svc => svc.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName);
            });

            return tokenClientBuilder;
        }

        /// <summary>
        /// Configures the logical token client with a custom method for getting the HttpClient.
        /// </summary>
        /// <param name="tokenClientBuilder">The ITokenClientBuilder.</param>
        /// <param name="getHttpClient">The delegate for getting the HttpClient instance.</param>
        /// <returns>The configured ITokenClientBuilder.</returns>
        public static IClientCredentialTokenClientBuilder UseHttpClientForBackchannel(this IClientCredentialTokenClientBuilder tokenClientBuilder, Func<IServiceProvider, HttpClient> getHttpClient)
        {
            if (tokenClientBuilder is null)
            {
                throw new ArgumentNullException(nameof(tokenClientBuilder));
            }

            if (getHttpClient is null)
            {
                throw new ArgumentNullException(nameof(getHttpClient));
            }

            tokenClientBuilder.Services.Configure<ClientCredentialsTokenClientOptions>(tokenClientBuilder.Name, o =>
            {
                o.GetBackchannelHttpClient = getHttpClient;
            });

            return tokenClientBuilder;
        }
    }
}
