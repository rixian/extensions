# Errors and Results

[![NuGet package](https://img.shields.io/nuget/v/Rixian.Extensions.Errors.svg)](https://nuget.org/packages/Rixian.Extensions.Errors)

## Overview

This library provides base types for working with errors deliberate way within your applications. The main types found in the library are:

- `Error`
- `Result`/`Result<T>/Success<T>/Fail`
- `HttpProblem`

## Features

- The `Error` object provides an implementation of the error response in the [Microsoft REST API Guidelines](https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md#errorresponse--object)
- The `Result` and `Result<T>` objects provide a mechanism for methods to return either a value or an error.
- The `HttpProblem` class represents an HttpResponse of type `application/problem+json`. See the [RFC](https://tools.ietf.org/html/rfc7807)
- A [`Prelude`](#prelude) class is provided to add additional helper methods to make working with Errors and Results easier.

## Usage

### Error

The `Error` class provides the basic properties required in the [Microsoft REST API Guidelines](https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md#errorresponse--object). This class explicitly allows for subclassing in order to create predefined errors or add extra properties beyond the basics. For example, we provide the `UnhandledError` class which looks like this (simplified):

```csharp
public class UnhandledError : Error
{
    public UnhandledError()
    {
        this.Code = "unhandled";
    }
}
```

You can also add other properties to expand on the error as needed:

```csharp
public class InvalidUserError : Error
{
    public InvalidUserError(string username)
    {
        this.Code = "invalid_user";
        this.UserName = username;
    }

    public string UserName { get; set; }
}
```

### Working with Errors

This library provides two main ways of working with errors: Using the Result classes to encapsulate errors, and tuples for errors in the style of the Go language.

#### Go language style

In Go, errors are conventionally the last item in a tuple. In C# we would do that by transforming the return type of a method. For example, let's take the following method:

```csharp
public double Add(double x, double y)
{
    return x + y;
}
```

Might look like this:

```csharp
public (double, Error?) Add(double x, double y)
{
 return (x + y, default);
}
```

You can use this method in a way that closely resembles Go:

```csharp
var (result, err) = Add(5, 6);
// OR
(double result, Error? err) = Add(5, 6);
```

You would then check if `err` is null and then proceed:

```csharp
var (result, err) = Add(5, 6);
if (err is null)
{
    // Do some error handling
}
else
{
    // Use the result
}
```

If you choose to use this style of error handling, then this library only provides minimal help around creating and managing the tuples. Since you still need an error type, we recommend using the `Error` type in this library as a base.

#### Note

We do provide several ways for this tuple to interoperate with the Result classes, making it so you are not locked into a single way of propagating errors in your code.

### Results

The `Result` and `Result<T>` classes are implementations of a discriminated union where it can either be an `Error` or a value. These classes exist to make error handling more explicit by having developers check if a `Result` contains an error before grabbing the value.

AN example usage may look like this:

```csharp
public class Calc
{
    public Result<double> Add(double x, double y)
    {
        return x + y; // Implicit conversion to Result<double>
    }

    public Success<double> AddGuarenteed(double x, double y)
    {
        return x + y; // Implicit conversion to Success<double>
    }

    public Result<double> Divide(double numerator, double denominator)
    {
        if (denominator == 0)
        {
            Error error = Error("divide_by_zero"); // Method provided by the Prelude class
            return error; // Implicit conversion to Result<double>.
        }

        return numerator / denominator;
    }
}
```

The main types used are:

- `Result` - This is the base type that indicates in there is a success or fail.
- `Success<T>` - Derives from `Result`, and indicates a success.
- `Fail` - Derives from `Result`, and indicates a fail.
- `Result<T>` - Derives from `Result` and indicates either a success or fail.

You can easily convert between these types, and so you should consider using the specific versions when known (i.e. guarenteed success). YHou will also find methods to instantiate these on the `Result` class, such as: `Result.New<int>(5)`.

### Prelude

The `Prelude` class makes use of the C# [using static directive](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-static). In your class add the following usings:

```csharp
using Rixian.Extensions.Errors;
using static Rixian.Extensions.Errors.Prelude;
```

You now have access to the following:

- `Error()`
- `Error<T>()`
- `BadArgumentError()`
- `NullArgumentDisallowedError()`
- `NullValueDisallowedError()`
- `EmptyGuidDisallowedError()`
- `Result<T>()`
- `NullResult<T>()`
- `RequireGuid()`
- `DefaultResult` (Readonly field)

### HttpProblem

The `HttpProblem` class and its sibling `HttpProblemError` are used for working with the [HTTP Problem RFC](https://tools.ietf.org/html/rfc7807). This class is intended to bridge the gap into the `Error` class to take advantage of those helpers.

# Rixian HttpClient Extensions

**This library provides extension methods for setting up an HttpClient.**

[![NuGet package](https://img.shields.io/nuget/v/Rixian.Extensions.Http.svg)](https://nuget.org/packages/Rixian.Extensions.Http)
[![codecov](https://codecov.io/gh/rixian/extensions-http/branch/master/graph/badge.svg)](https://codecov.io/gh/rixian/extensions-http)

## Features

- Configure Basic HttlClient properties, such as SSL Protocols.
- Configure an [ITokenClient](https://github.com/rixian/extensions-tokens) as the source of the Authorization header.
- Configure the Authorization header with a static Bearer token value.
- Configure any header with a static value.

## Usage

### Use SSL Protocols

```csharp
IServiceCollection services = ...;

services.AddHttpClient("test")
    .UseSslProtocols(SslProtocols.Tls12); // Sets the primary HttpClientHandler to use TLS 1.2
```

### ITokenClient - Named

```csharp
IServiceCollection services = ...;

services.AddTokenClient("tokenClient123", ...);

services.AddHttpClient("test")
    .AddTokenClient("tokenClient123"); // Adds the header "Authorization: Bearer {access_token}"
```

### ITokenClient - DI Lookup

```csharp
IServiceCollection services = ...;

services.AddTokenClient(...);

services.AddHttpClient("test")
    .AddTokenClient(svc =>
    {
        return svc
            .GetRequiredService<ITokenClientFactory>()
            .GetTokenClient();
    }); // Adds the header "Authorization: Bearer {access_token}"
```

### ITokenClient - Direct Instance

```csharp
IServiceCollection services = ...;

ITokenClient tokenClient = ...;

services.AddHttpClient("test")
    .AddTokenClient(tokenClient); // Adds the header "Authorization: Bearer {access_token}"
```

### Static Bearer Token

```csharp
IServiceCollection services = ...;

string bearerToken = "REPLACE_ME";

services.AddHttpClient("test")
    .AddBearerToken(bearerToken); // Adds the header "Authorization: Bearer REPLACE_ME"
```

### Static Authorization Header

```csharp
IServiceCollection services = ...;

string scheme = "HDR_SCHEME";
string parameter = "HDR_PARAM";

services.AddHttpClient("test")
    .AddAuthorizationHeader(scheme, parameter); // Adds the header "Authorization: HDR_SCHEME HDR_PARAM"
```

### Static Header

```csharp
IServiceCollection services = ...;

string name = "HDR_NAME";
string value = "HDR_VALUE";

services.AddHttpClient("test")
    .AddHeader(name, value); // Adds the header "HDR_NAME: HDR_VALUE"
```
