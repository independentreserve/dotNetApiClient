# Independent Reserve .NET API Client

![NuGet](https://img.shields.io/nuget/v/independentreserve.client.svg)

This .NET client encapsulates the private and public methods exposed by the [Independent Reserve](https://www.independentreserve.com) API.

It targets .NET Standard 2.0

## Installation

Install the latest package [via nuget](https://www.nuget.org/packages/IndependentReserve.Client/)

## Development

Requires `Visual Studio 2022+`

### Unit Tests

Tests require a single environment variable set with a CSV of `baseUrl`, `apiKey` and `secret`.

This can be set from an elevated powershell prompt like so:

`[Environment]::SetEnvironmentVariable("IR_DOTNETCLIENTAPI_TEST_CONFIG", "<url>,<key>,<secret>", "Machine")`

### Sample Application

This simple WPF application demonstrates usage and responses

Specify your API Key and API Secret in the configuration file `SampleApplication.exe.config`.
You can switch between expiry modes by changing `expiryMode` to `Nonce` or `Timestamp` in the configuration file.  

## Further Help

See the [API documentation](https://www.independentreserve.com/API) for more information.

## Backward compatibility

Sometimes we will list a new currency that this client isn't aware of (say when you haven't updated the nuget package for some time).
In this case, a 32-bit `FNV-1a` will be substituted for the new and unknown (to the client) `CurrencyCode`.

If you see these `FNV-1a` values then please update to the latest nuget package.
