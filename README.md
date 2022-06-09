# Independent Reserve .NET API Client

![NuGet](https://img.shields.io/nuget/v/independentreserve.client.svg)

This .NET client encapsulates the private and public methods exposed by the [Independent Reserve](https://www.independentreserve.com) API.

It targets .NET Standard 2.0

## Installation

Install the latest package [via nuget](https://www.nuget.org/packages/IndependentReserve.Client/)

## Development

Requires `Visual Studio 2017+`

### Unit Tests

Tests require a single environment variable set with a CSV of baseUrl, apiKey and secret.

This can be set from an elevated powershell prompt like so:

`[Environment]::SetEnvironmentVariable("IR_DOTNETCLIENTAPI_TEST_CONFIG", "<url>,<key>,<secret>", "Machine")`


### Sample Application

The sample application shows how to use the client in a simple WPF application.

Specify your API Key and API Secret in the Sample Application configuration file `SampleApplication.exe.config`.

## Further Help

See the [API documentation](https://www.independentreserve.com/API) for more information.

## Backward compatibility
After the listing of a new currency that doesn't exist in the `CurrencyCode` enum, a `FNV-1a` hash (32-bit) of a listed currency ticker will be returned as a `CurrencyCode`. It is done for better compatibility with 3-rd party clients that, for instance, use `ToDictionary` by `CurrencyCode`.