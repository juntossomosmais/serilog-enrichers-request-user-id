# Serilog.Enrichers.RequestUserIid

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=juntossomosmais_serilog-enrichers-request-user-id&metric=coverage)](https://sonarcloud.io/dashboard?id=juntossomosmais_serilog-enrichers-request-user-id)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=juntossomosmais_serilog-enrichers-request-user-id&metric=alert_status)](https://sonarcloud.io/dashboard?id=juntossomosmais_serilog-enrichers-request-user-id)

Enrich Serilog log events with the request's user Id from ClaimsPrincipal.

Apply the enricher to your `LoggerConfiguration` in code:

```c#
Log.Logger = new LoggerConfiguration()
    .Enrich.WithRequestUserId()
    // ...other configuration...
    .CreateLogger()
```

or in the `appsettings.json` file:

```json
{
  "Serilog": {
    "MinimumLevel": "Information",
    "Using":  ["Serilog.Enrichers.RequestUserId"],
    "Enrich": ["WithRequestUserId"],
    "WriteTo": [
      {
        "Name": "Console"
      }
    ]
  }
}
```

As a result the `RequestUserId` property will be added to the log events with the content of `ClaisPrincipal` with the key `uid` or `cid`.

You need to register the `IHttpContextAccessor` singleton so that the enricher has access to the `ClaimsPrincipal`.