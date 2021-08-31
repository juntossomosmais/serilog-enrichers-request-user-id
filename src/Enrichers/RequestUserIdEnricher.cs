using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enrich log events with a RequestUserId property containing the value of the user Id on ClaimsPrincipal.
    /// </summary>
    public class RequestUserIdEnricher : ILogEventEnricher
    {
        private const string UserIdPropertyName = "RequestUserId";
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Creates a  RequestUserIdEnricher
        /// </summary>
        public RequestUserIdEnricher() : this(new HttpContextAccessor())
        {
        }

        /// <summary>
        /// Creates a  RequestUserIdEnricher
        /// </summary>
        public RequestUserIdEnricher(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_contextAccessor.HttpContext == null)
                return;

            var userId = _contextAccessor.HttpContext.User.GetUserId();

            var correlationIdProperty = new LogEventProperty(UserIdPropertyName, new ScalarValue(userId));

            logEvent.AddOrUpdateProperty(correlationIdProperty);
        }
    }
}