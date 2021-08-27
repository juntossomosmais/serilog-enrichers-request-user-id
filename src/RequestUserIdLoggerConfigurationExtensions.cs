using Serilog.Configuration;
using System;

namespace Serilog.Enrichers.RequestUserId
{
    public static class RequestUserIdLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithRequestUserId(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration is null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<RequestUserIdEnricher>();
        }
    }
}