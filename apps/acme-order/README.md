# ACME Order Service

Requires [.NET SDK 10](https://dotnet.microsoft.com/download).

## Getting Started

Download the [.NET SDK](https://dotnet.microsoft.com/download)

In Visual Studio Code: install the [**C# Dev Kit** extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

## Locally running acme-order

Ensure local development dependencies are running (see [local-development README](../../local-development/README.md)).

Start the application:

```bash
dotnet run --urls=http://localhost:8086/
```

Verify the health of the application:

```bash
open localhost:8086/actuator/health
```

## Deploying acme-order app

### Build deployable

```bash
dotnet publish -r linux-x64
```

### Deploy on Tanzu Platform

Included [manifest.yml](./manifest.yml) file can be used to deploy the published binary

Ensure you're logged in to your TAS instance on cf cli

```bash
cf login -a <your-tas-api-url>
cf push -f manifest.yml
```
