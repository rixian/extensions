// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Authentication;
    using Microsoft.Extensions.Logging;
    using Rixian.Extensions.Errors;
    using Rixian.Extensions.Http;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Provides extension methods for configuring an HttpClient.
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// Configures the HttpClient to use the AccessToken retrieved from an ITokenClient in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="tokenClientName">The logical name of the ITokenClient.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseTokenClient(this IHttpClientBuilder httpClientBuilder, string tokenClientName)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc =>
            {
                ITokenClientFactory tokenClientFactory = svc.GetRequiredService<ITokenClientFactory>();
                if (tokenClientFactory == null)
                {
                    throw new HttpClientConfigurationException("No ITokenClientFactory registered in the DI container.");
                }

                Result<ITokenClient> tokenClient = tokenClientFactory.GetTokenClient(tokenClientName);
                if (tokenClient.IsSuccess == false)
                {
                    throw new HttpClientConfigurationException(string.Format(CultureInfo.InvariantCulture, "No ITokenClient registered with the name '{0}'.", tokenClientName));
                }

                var handler = new TokenClientDelegatingHandler(tokenClient.Value!, svc.GetRequiredService<ILogger<TokenClientDelegatingHandler>>());
                return handler;
            });
        }

        /// <summary>
        /// Configures the HttpClient to use the AccessToken retrieved from an ITokenClient in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="getTokenClient">Delegate that pulls the ITokenCLient instance from the DI container.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseTokenClient(this IHttpClientBuilder httpClientBuilder, Func<IServiceProvider, Result<ITokenClient>> getTokenClient)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc =>
            {
                Result<ITokenClient> result = getTokenClient(svc);
                if (result.TryGetValue(out ITokenClient? client) && client is object)
                {
                    return new TokenClientDelegatingHandler(client, svc.GetRequiredService<ILogger<TokenClientDelegatingHandler>>());
                }
                else
                {
                    throw new ErrorException(result.Error);
                }
            });
        }

        /// <summary>
        /// Configures the HttpClient to use the AccessToken retrieved from an ITokenClient in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="tokenClient">The ITokenClient to use.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseTokenClient(this IHttpClientBuilder httpClientBuilder, ITokenClient tokenClient)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc => new TokenClientDelegatingHandler(tokenClient, svc.GetRequiredService<ILogger<TokenClientDelegatingHandler>>()));
        }

        /// <summary>
        /// Configures the HttpClient to use the bearer token in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseBearerToken(this IHttpClientBuilder httpClientBuilder, string bearerToken)
        {
            return httpClientBuilder.UseAuthorizationHeader("Bearer", bearerToken);
        }

        /// <summary>
        /// Configures the HttpClient to use a specific Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="scheme">The scheme to use.</param>
        /// <param name="parameter">The auth value to place in the header.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseAuthorizationHeader(this IHttpClientBuilder httpClientBuilder, string scheme, string parameter)
        {
            return httpClientBuilder.ConfigureHttpClient(client => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter));
        }

        /// <summary>
        /// Configures the HttpClient to use a specific header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="name">The name of the header.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseHeader(this IHttpClientBuilder httpClientBuilder, string name, string value)
        {
            return httpClientBuilder.ConfigureHttpClient(client => client.DefaultRequestHeaders.TryAddWithoutValidation(name, value));
        }

        /// <summary>
        /// Configures the primary HttpClientHandler to use the specified SslProtocols.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="sslProtocols">The SslProtocols to use.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseSslProtocols(this IHttpClientBuilder httpClientBuilder, SslProtocols sslProtocols)
        {
            return httpClientBuilder.ConfigureHttpMessageHandlerBuilder(b =>
            {
                if (b?.PrimaryHandler is HttpClientHandler httpClientHandler)
                {
                    httpClientHandler.SslProtocols = sslProtocols;
                }
            });
        }

        /// <summary>
        /// Configures the primary HttpClientHandler to use the specified MaxRequestContentBufferSize.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="maxRequestContentBufferSize">The MaxRequestContentBufferSize.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseMaxRequestContentBufferSize(this IHttpClientBuilder httpClientBuilder, long maxRequestContentBufferSize)
        {
            return httpClientBuilder.ConfigureHttpMessageHandlerBuilder(b =>
            {
                if (b?.PrimaryHandler is HttpClientHandler httpClientHandler)
                {
                    httpClientHandler.MaxRequestContentBufferSize = maxRequestContentBufferSize;
                }
            });
        }

        /// <summary>
        /// Configures the primary HttpClientHandler to use the specified api version as a query parameter.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="version">The api version to use.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseApiVersion(this IHttpClientBuilder httpClientBuilder, string version)
        {
            var options = new ApiVersionQueryOptions
            {
                Value = version,
            };

            return httpClientBuilder.AddHttpMessageHandler(() => new ApiVersionQueryParamDelegatingHandler(Options.Options.Create(options)));
        }

        /// <summary>
        /// Configures the primary HttpClientHandler to use the specified api version as a query parameter.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClientBuilder.</param>
        /// <param name="version">The api version to use.</param>
        /// <param name="queryParamName">The name of the query parameter.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder UseApiVersion(this IHttpClientBuilder httpClientBuilder, string version, string queryParamName)
        {
            var options = new ApiVersionQueryOptions
            {
                Value = version,
            };

            if (!string.IsNullOrWhiteSpace(queryParamName))
            {
                options.QueryParamName = queryParamName!;
            }

            return httpClientBuilder.AddHttpMessageHandler(() => new ApiVersionQueryParamDelegatingHandler(Options.Options.Create(options)));
        }
    }
}
