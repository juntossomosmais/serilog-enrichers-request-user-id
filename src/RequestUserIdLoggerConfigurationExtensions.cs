using Serilog.Configuration;
using Serilog.Enrichers;
using System;

namespace Serilog
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