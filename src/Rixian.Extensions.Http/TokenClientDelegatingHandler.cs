// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rixian.Extensions.Errors;
using Rixian.Extensions.Tokens;

/// <summary>
/// Configures the Authentication header with the Bearer scheme and uses the AccessToken property of the ITokenClient.
/// </summary>
public class TokenClientDelegatingHandler : DelegatingHandler
{
    private readonly ILogger logger;
    private readonly ITokenClient tokenClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenClientDelegatingHandler"/> class.
    /// </summary>
    /// <param name="tokenClient">The ITokenClient to use.</param>
    /// <param name="logger">The ILogger instance.</param>
    public TokenClientDelegatingHandler(ITokenClient tokenClient, ILogger logger)
    {
        this.tokenClient = tokenClient;
        this.logger = logger;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        Result<ITokenInfo> tokenResult = await this.tokenClient.GetTokenAsync().ConfigureAwait(false);
        if (tokenResult.TryGetValue(out ITokenInfo token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }
        else
        {
            this.logger.LogError(Properties.Resources.FailedToRetrieveTokenErrorMessage, tokenResult.Error.Code, tokenResult.Error.Target, tokenResult.Error.Message, tokenResult.Error.Details);
        }

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
