using Serilog.Configuration;
using Serilog.Enrichers;
using System;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add enrichers for request's UserId.
    /// </summary>
    public static class RequestUserIdLoggerConfigurationExtensions
    {
        /// <summary>
        /// Enrich log events with a RequestUserId property containing the value of the user Id on ClaimsPrincipal.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithRequestUserId(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration is null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<RequestUserIdEnricher>();
        }
    }
}